using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Module2
{
    public class VendingMachine
    {
        public int SnacksLeft { get; private set; }
        public State State { get; private set; }

        public VendingMachine(int numberOfSnacks)
        {
            if (numberOfSnacks < 1)
                throw new Exception("The number of snacks must be positive");

            State = State.AcceptingPayment;
            SnacksLeft = numberOfSnacks;
        }

        public void InsertDollar()
        {
            if (State == State.DispensingSnack || State == State.Empty)
                return;

            if (State != State.AcceptingPayment)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            State = State.DispensingSnack;
        }

        public void DispenseSnack()
        {
            if (State == State.AcceptingPayment || State == State.Empty)
                return;

            if (State != State.DispensingSnack)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            SnacksLeft--;
            if (SnacksLeft > 0)
            {
                State = State.AcceptingPayment;
            }
            else
            {
                State = State.Empty;
            }
        }

        public void LoadSnacks(int numberOfSnacks)
        {
            if (State == State.AcceptingPayment || State == State.DispensingSnack)
                return;

            if (State != State.Empty)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            SnacksLeft += numberOfSnacks;
            State = State.AcceptingPayment;
        }
    }

    public class State : ValueObject
    {
        public static readonly State AcceptingPayment = new State("Accepting payment");
        public static readonly State DispensingSnack = new State("Dispensing a snack");
        public static readonly State Empty = new State("Empty");

        public string Name { get; }

        private State(string name)
        {
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
