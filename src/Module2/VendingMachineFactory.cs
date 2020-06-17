namespace Module2
{
    public static class VendingMachineFactory
    {
        public static VendingMachine CreateAcceptingPaymentMachine(int numberOfSnacks = 3)
        {
            var machine = new VendingMachine(numberOfSnacks);
            return machine;
        }

        public static VendingMachine CreateDispensingSnackMachine(int numberOfSnacks = 3)
        {
            VendingMachine machine = CreateAcceptingPaymentMachine(numberOfSnacks);
            machine.InsertDollar();
            return machine;
        }

        public static VendingMachine CreateEmptyMachine()
        {
            VendingMachine machine = CreateDispensingSnackMachine(1);
            machine.DispenseSnack();
            return machine;
        }
    }
}
