using System;

namespace Module3
{
    public class InquiryController
    {
        private readonly InquiryRepository _repository;
        private readonly IDateTimeServer _timeServer;

        public InquiryController(InquiryRepository repository, IDateTimeServer timeServer)
        {
            _repository = repository;
            _timeServer = timeServer;
        }

        public string Add(int id)
        {
            var inquiry = new Inquiry(id, false, null);
            _repository.Add(inquiry);

            return "OK";
        }

        public string Approve(int id)
        {
            Inquiry inquiry = _repository.GetById(id);
            if (inquiry == null)
                return "Not found";

            inquiry.Approve(_timeServer.Now);
            _repository.Update(inquiry);

            return "OK";
        }

        public string Delete(int id)
        {
            _repository.Delete(id);
            return "OK";
        }
    }

    public interface IDateTimeServer
    {
        DateTime Now { get; }
    }

    public class DateTimeServer : IDateTimeServer
    {
        public DateTime Now => DateTime.Now;
    }

    public class InquiryRepository
    {
        public void Add(Inquiry inquiry)
        {
            // Use Dapper with an INSERT SQL statement
        }

        public void Delete(int id)
        {
            // Use Dapper with a DELETE SQL statement
        }

        public Inquiry GetById(int id)
        {
            // Use Dapper with a SELECT SQL statement

            return new Inquiry(id, false, null);
        }

        public void Update(Inquiry inquiry)
        {
            // Use Dapper with an UPDATE SQL statement
        }
    }
}
