using System;

namespace Module3
{
    public class InquiryController
    {
        private readonly InquiryRepository _repository;

        public InquiryController(InquiryRepository repository)
        {
            _repository = repository;
        }

        public string Add(int id)
        {
            bool isApproved = false;
            DateTime? timeApproved = null;

            _repository.Add(id, isApproved, timeApproved);

            return "OK";
        }

        public string Approve(int id)
        {
            object[] data = _repository.GetById(id);
            if (data == null)
                return "Not found";

            bool isApproved = (bool)data[0];

            if (!isApproved)
            {
                _repository.Update(id, true, DateTime.Now);
            }

            return "OK";
        }

        public string Delete(int id)
        {
            _repository.Delete(id);
            return "OK";
        }
    }

    public class InquiryRepository
    {
        public void Add(int id, bool isApproved, DateTime? timeApproved)
        {
            // Use Dapper with an INSERT SQL statement
        }

        public void Delete(int id)
        {
            // Use Dapper with a DELETE SQL statement
        }

        public object[] GetById(int id)
        {
            // Use Dapper with a SELECT SQL statement

            return new object[] { false, null };
        }

        public void Update(int id, bool isApproved, DateTime? timeApproved)
        {
            // Use Dapper with an UPDATE SQL statement
        }
    }
}
