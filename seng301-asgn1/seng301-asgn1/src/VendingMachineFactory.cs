using System.Collections;
using System.Collections.Generic;
using System;

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

        private List<VendingMachine> machineList = new List<VendingMachine>();


        public VendingMachineFactory() {
            //System.Console.WriteLine("vending Machine Factory is run");
           
        }

        public int createVendingMachine(List<int> coinKinds, int selectionButtonCount) {

            VendingMachine tempMachine = new VendingMachine(coinKinds, selectionButtonCount);
            machineList.Add(tempMachine);
            return 0;
        }

        public void configureVendingMachine(int vmIndex, List<string> popNames, List<int> popCosts) {
            if (machineList[vmIndex].GetType() == typeof(VendingMachine))   //type checking son.
            {
                machineList[vmIndex].addPopTypes(popNames, popCosts);
            }
            else
            {
                throw new Exception("This machine doesn't exist");
            }
            //System.Console.WriteLine("configure Vending Machine");

        }

        public void loadCoins(int vmIndex, int coinKindIndex, List<Coin> coins) {
            machineList[vmIndex].addCoins(coins[0].Value, coinKindIndex, coins.Count);
            //System.Console.WriteLine("load Coins is run");
        }

        public void loadPops(int vmIndex, int popKindIndex, List<Pop> pops) {
            machineList[vmIndex].addPops(pops[0].ToString(), popKindIndex, pops.Count);
            //System.Console.WriteLine("loadPops is run");
            
        }

        public void insertCoin(int vmIndex, Coin coin) {
            machineList[vmIndex].inputCoin(coin);
            //System.Console.WriteLine("insertCoins is run");
        }

        public void pressButton(int vmIndex, int value) {
            machineList[vmIndex].pressButton(value);
            //System.Console.WriteLine("pressButton is run");
        }

        public List<Deliverable> extractFromDeliveryChute(int vmIndex) {
            List<Deliverable> tempList = machineList[vmIndex].extractFromChute();
            //System.Console.WriteLine("extractFromDeliveryChute is run");
            return tempList;
        }

        public List<IList> unloadVendingMachine(int vmIndex) {
            List<IList> tempList = machineList[vmIndex].teardownMachine();
            //System.Console.WriteLine("unloadVendingMachine is run");
            return tempList;
            }
    }
    
}