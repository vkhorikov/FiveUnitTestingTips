using System;

namespace Module3
{
    public class Inquiry
    {
        public int Id { get; }
        public bool IsApproved { get; private set; }
        public DateTime? TimeApproved { get; private set; }

        public Inquiry(int id, bool isApproved, DateTime? timeApproved)
        {
            if (isApproved && !timeApproved.HasValue)
                throw new Exception();

            Id = id;
            IsApproved = isApproved;
            TimeApproved = timeApproved;
        }

        public void Approve(DateTime now)
        {
            if (IsApproved)
                return;

            IsApproved = true;
            TimeApproved = now;
        }
    }
}
