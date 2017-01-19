using System.Collections;
using System.Collections.Generic;

using Frontend1;

namespace seng301_asgn1 {
    /// <summary>
    /// Represents the concrete virtual vending machine factory that you will implement.
    /// This implements the IVendingMachineFactory interface, and so all the functions
    /// are already stubbed out for you.
    /// 
    /// Your task will be to replace the TODO statements with actual code.
    /// 
    /// Pay particular attention to extractFromDeliveryChute and unloadVendingMachine:
    /// 
    /// 1. These are different: extractFromDeliveryChute means that you take out the stuff
    /// that has already been dispensed by the machine (e.g. pops, money) -- sometimes
    /// nothing will be dispensed yet; unloadVendingMachine is when you (virtually) open
    /// the thing up, and extract all of the stuff -- the money we've made, the money that's
    /// left over, and the unsold pops.
    /// 
    /// 2. Their return signatures are very particular. You need to adhere to this return
    /// signature to enable good integration with the other piece of code (remember:
    /// this was written by your boss). Right now, they return "empty" things, which is
    /// something you will ultimately need to modify.
    /// 
    /// 3. Each of these return signatures returns typed collections. For a quick primer
    /// on typed collections: https://www.youtube.com/watch?v=WtpoaacjLtI -- if it does not
    /// make sense, you can look up "Generic Collection" tutorials for C#.
    /// </summary>
    public class VendingMachineFactory : IVendingMachineFactory {

        public VendingMachineFactory() {
            // TODO: Implement
            System.Console.WriteLine("vending Machine is run");
        }

        public int createVendingMachine(List<int> coinKinds, int selectionButtonCount) {
            // TODO: Implement
            System.Console.WriteLine("create Vending Machine");
            return 0;
        }

        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {
            // TODO: Implement
            System.Console.WriteLine("configure Vending Machine");
        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {
            // TODO: Implement
            //open door
            //insert coin
            //verify coin
            //increase coin count in machine
            System.Console.WriteLine("load Coins is run");
        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {
            // TODO: Implement
            System.Console.WriteLine("loadPops is run");
        }

        public void insertCoin(int vmIndex, Coin coin) {
            // TODO: Implement
            System.Console.WriteLine("insertCoins is run");
        }

        public void pressButton(int vmIndex, int value) {
            // TODO: Implement
            System.Console.WriteLine("pressButton is run");
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex) {
            // TODO: Implement
            System.Console.WriteLine("extractFromDeliveryChute is run");
            return new List<Deliverable>();
        }

        public List<IList> unloadVendingMachine(int vmIndex) {
            // TODO: Implement
            System.Console.WriteLine("unloadVendingMachine is run");
            return new List<IList>() {
                new List<Coin>(),
                new List<Coin>(),
                new List<Pop>() };
            }
    }
}