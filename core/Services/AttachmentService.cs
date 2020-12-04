using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


//logg
using Serilog;
using System.IO;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using hello.transaction.core.Extensions;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Xml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using AutoMapper;
using hello.transaction.core.Models;
using hello.transaction.core.Repositories;
using FluentValidation;
using hello.transaction.core.Exceptions;

//using FluentValidation;

namespace hello.transaction.core.Services
{

    public interface IAttachmentService
    {
        Task<Attachment> CreateAsync(Attachment attachments);
        //Task<Attachment> UpdateAsync(Attachment attachments);
        //Task<Attachment> DeleteAsync(string id);

        Task<Attachment> GetAsync(string id);

        Task<IEnumerable<Attachment>> ListAsync();
        Task<Attachment> EnforceAttachmentExistenceAsync(string id);
        Task<IEnumerable<Transaction>> ExtractAttachmentAsync(Attachment attachment);

    }

    public class AttachmentService : IAttachmentService
    {
        //attachment status
        protected readonly IGenericRepository<Attachment, string> _attachmentRepository;
        private readonly AbstractValidator<Transaction> _transactionValidator;
        private readonly IMapper _autoMapperService;
        private readonly ITransactionService _transactionService;

        public AttachmentService(IGenericRepository<Attachment, string> attachmentRepository, IMapper autoMapperService, AbstractValidator<Transaction> transactionValidator, ITransactionService transactionService)
        {
            _attachmentRepository = attachmentRepository ?? throw new ArgumentNullException(nameof(attachmentRepository));
            _autoMapperService = autoMapperService ?? throw new ArgumentNullException(nameof(autoMapperService));
            _transactionValidator = transactionValidator ?? throw new ArgumentNullException(nameof(transactionValidator));

            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));

            //logg
            Log.Logger = new LoggerConfiguration()
                            .WriteTo.Console()
                            .CreateLogger();
        }

        //
        // Summary:
        //      create attachemnt record with  Anonymous
        //
        // Returns:
        //      attachemnt model
        //
        // Params:
        //      attachment model with base64 content
        public async Task<Attachment> CreateAsync(Attachment attachment)
        {
            //validate attachment
            if (attachment.Size > 1024)
            {
                throw new AttachmentNotValidException("Maximum file size exceeded");
            }

            if (attachment.Base64Content == null)
            {
                throw new AttachmentNotValidException("Null content");
            }

            var EXTENSION = new string[] { ".csv", ".xml" };
            if (!EXTENSION.Contains(attachment.Extension))
            {
                throw new AttachmentNotValidException("Unknown format");
            }

            //set reference id
            attachment.Id = Guid.NewGuid().ToString();

            //extract
            var transactions = await this.ExtractAttachmentAsync(attachment);

            //validate each transaction
            foreach (var t in transactions)
            {
                var validate = _transactionValidator.Validate(t);
                if (!validate.IsValid)
                {
                    throw new AttachmentNotValidException("Invalid record");
                };
            }

            //persiste attachemt
            attachment.Date = DateTime.UtcNow;
            attachment.By = "Anonymous";
            attachment.Status = AttachmentStatus.VALID.GetDescription();

            attachment.StorageType = StorageType.DB.GetDescription();

            var entity = await _attachmentRepository.CreateAsync(attachment);
            if (entity == null)
            {
                throw new AttachmentNotCreatedException();
            }

            //set attachment id for tracking
            transactions = transactions
                    .Select(c => { c.AttachmentId = attachment.Id; return c; }).ToList();

            //persiste transaction
            var entities = await _transactionService.CreateRangeAsync(transactions);

            attachment.LoadedTransaction = entities;

            return attachment;
        }


        //
        // Summary:
        //      extract attachemnt by id and type of file
        //
        // Returns:
        //      list of valid transction 
        //
        // Params:
        //      attachment id
        public async Task<IEnumerable<Transaction>> ExtractAttachmentAsync(Attachment attachment)
        {
            await Task.Delay(0);

            //stream file to memory
            Byte[] bytes = attachment.Base64Content;

            using (var stream = new MemoryStream(bytes))
            {
                using (var reader = new StreamReader(stream))
                {

                    //csv
                    if (attachment.Extension == ".csv")
                    {
                        // Read file content.
                        //var content = reader.ReadToEnd();
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            csv.Configuration.BadDataFound = null;
                            csv.Configuration.TrimOptions = TrimOptions.Trim;

                            var entities = new List<Transaction>();
                            //csv.Read();
                            //csv.ReadHeader();
                            while (csv.Read())
                            {
                                try
                                {
                                    var record = new Transaction
                                    {
                                        Id = csv.GetField(0),
                                        Amount = Convert.ToDecimal(csv.GetField(1)),

                                        CurrencyCode = csv.GetField(2),
                                        TransactionDate = DateTime.ParseExact(csv.GetField(3), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture),

                                        Status = csv.GetField(4),

                                        //for validate status
                                        FormatType = FormatType.CSV.GetDescription()

                                    };

                                    entities.Add(record);
                                }
                                catch (FormatException) { throw new AttachmentNotValidException("Invalid record"); }

                            }

                            return entities;
                        }

                    }

                    //xml
                    else if (attachment.Extension == ".xml")
                    {
                        var content = reader.ReadToEnd();

                        //remove newline
                        string replacement = Regex.Replace(content, @"\t|\n|\r", "").Trim();

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(replacement);

                        //convert to json
                        string json = JsonConvert.SerializeXmlNode(doc);
                        var transactionXmls = JsonConvert.DeserializeObject<TransactionDto>(json);

                        //mapper to transaction
                        var entities = _autoMapperService.Map<List<Transaction>>(transactionXmls.Transactions.Transaction);
                        return entities;

                    }
                    else
                    {
                        throw new AttachmentNotValidException();
                    }
                }
            }
        }


        public async Task<Attachment> GetAsync(string id)
        {
            var entity = await _attachmentRepository.GetAsync(id);
            return entity;
        }


        public async Task<IEnumerable<Attachment>> ListAsync()
        {
            return await _attachmentRepository.ListAsync();
        }

        public async Task<Attachment> EnforceAttachmentExistenceAsync(string id)
        {
            var attachment = await _attachmentRepository.GetAsync(id);

            if (attachment == null)
            {
                throw new AttachmentNotFoundException();
            }

            return attachment;
        }

    }

}