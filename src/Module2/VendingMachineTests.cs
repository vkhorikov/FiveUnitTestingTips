using System;
using FluentAssertions;
using Xunit;
using static Module2.VendingMachineFactory;

namespace Module2
{
    public class VendingMachineTests
    {
        [Fact]
        public void A_new_vending_machine_resides_in_AcceptingPayment_state()
        {
            int numberOfSnacks = 3;

            var machine = new VendingMachine(numberOfSnacks);

            machine.State.Should().Be(State.AcceptingPayment);
            machine.SnacksLeft.Should().Be(numberOfSnacks);
        }

        [Fact]
        public void Cannot_create_a_vending_machine_without_snacks()
        {
            int numberOfSnacks = 0;

            Action action = () => new VendingMachine(numberOfSnacks);

            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Vending_machine_is_ready_to_dispense_a_snack_after_accepting_payment()
        {
            var machine = CreateAcceptingPaymentMachine();

            machine.InsertDollar();

            machine.State.Should().Be(State.DispensingSnack);
        }

        [Fact]
        public void Vending_machine_is_empty_after_last_snack_is_dispensed()
        {
            var machine = CreateDispensingSnackMachine(1);
            
            machine.DispenseSnack();

            machine.State.Should().Be(State.Empty);
        }

        [Fact]
        public void Reloading_snacks_allows_vending_machine_to_accept_new_payments()
        {
            var machine = CreateEmptyMachine();

            machine.LoadSnacks(3);

            machine.State.Should().Be(State.AcceptingPayment);
            machine.SnacksLeft.Should().Be(3);
        }
    }
}
