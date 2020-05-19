using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace App2
{
    public class VendingMachine
    {
        public int SnacksLeft { get; private set; }
        public State State { get; private set; }

        public VendingMachine()
        {
            State = State.ReadyToAcceptPayment;
            SnacksLeft = 3;
        }

        public void InsertDollar()
        {
            if (State == State.ReadyToDispenseSnack || State == State.Empty)
                return;

            if (State != State.ReadyToAcceptPayment)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            State = State.ReadyToDispenseSnack;
        }

        public void DispenseSnack()
        {
            if (State == State.ReadyToAcceptPayment || State == State.Empty)
                return;

            if (State != State.ReadyToDispenseSnack)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            SnacksLeft--;
            if (SnacksLeft > 0)
            {
                State = State.ReadyToAcceptPayment;
            }
            else
            {
                State = State.Empty;
            }
        }

        public void LoadSnacks(int numberOfSnacks)
        {
            if (State == State.ReadyToAcceptPayment || State == State.ReadyToDispenseSnack)
                return;

            if (State != State.Empty)
                throw new InvalidOperationException($"Unknown state: {State.Name}");

            SnacksLeft += numberOfSnacks;
            State = State.ReadyToAcceptPayment;
        }
    }

    public class State : ValueObject
    {
        public static readonly State ReadyToAcceptPayment = new State("Ready to accept payment");
        public static readonly State ReadyToDispenseSnack = new State("Ready to dispense a snack");
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
