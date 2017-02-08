using System.Collections;
using System.Collections.Generic;
using System;
using Frontend1;



namespace seng301_asgn1
{

    public class VendingMachine
    {

        private int buttonCount;                //Number of buttons for pops
        private int changeAvailable = 0;        //Total change in the machine
        private List<Coin> moneyRecieved = new List<Coin>();          //money recieved from purchases
        private int availableCredit = 0;        //amount the user has to purchase stuff.
        private Queue<Coin>[] coinChute;        //chutes of the coins
        private int[] coinKinds;                //types of coins accepted
        private int[] paymentOrder;             //a copy of the coinKinds, sorted to the largest coin's index at 0.
        private Dictionary<string, int> pops = new Dictionary<string, int>();   //has costs of pops
        private int[] popKinds;                 //costs of pops in chute
        private Queue<Pop>[] chuteList;         //array of queues that hold the pops. Hell of a structure.
        private List<Deliverable> deliveryChute = new List<Deliverable>(); // list of stuff to be extracted.     


        public VendingMachine(List<int> coinKinds, int buttonCount)
        {
            coinChute = new Queue<Coin>[coinKinds.Count];
            for (int p = 0; p < coinKinds.Count; p++)
            {
                coinChute[p] = new Queue<Coin>();           //initialize chutes of coins
            }
            this.coinKinds = new int[coinKinds.Count];        //initialize array of values of coins
            paymentOrder = new int[coinKinds.Count];            //initialize array that stores the index number of the coins for change.
            for(int i=0; i<coinKinds.Count; i++)                //copy over the coinKinds list
            {
                this.coinKinds[i] = coinKinds[i];
            }
            createPaymentOrder(this.coinKinds);
            this.buttonCount = buttonCount;
        }

        //creates an array based off of the number of coins and contains the indexes of the highest coin chutes
        private void createPaymentOrder(int[] coinArray)
        {
            int[] tempCoinArray = new int[coinKinds.Length];

            for (int q = 0; q < coinKinds.Length; q++)
            {
                tempCoinArray[q] = coinKinds[q];
            }
            Array.Sort(tempCoinArray);
            Array.Reverse(tempCoinArray);

            for (int w=0; w<coinKinds.Length; w++)
            {
                //for (int r=0)

                int r = 0;
                while (coinKinds[r] != tempCoinArray[w])
                {
                    r++;
                }
                paymentOrder[w] = r;
            }

        }

        //method to check if a coin is accepted in the machine
        public bool validCoin(int coin)
        {
            bool isValid = false;
            foreach (int i in coinKinds)
            {
                if (i == coin)
                {
                    isValid = true;
                }
            }
            return isValid;
        }

        //method to add coins to the change chutes
        public void addCoins(int coinType, int chuteNum, int numCoins)
        {
            if (!validCoin(coinType))
            {
                throw new Exception("This coin is not a valid coin for this machine");
            }
            else
            {
                
                for (int c = 0; c < numCoins; c++)      //add coins to the respective chute
                {
                    coinChute[chuteNum].Enqueue(new Coin(coinType));
                }
                changeAvailable = (changeAvailable + (numCoins * coinType));    //update total change available
            }
        }

        //inputs a coin 
        public void inputCoin(Coin theCoin)
        {
            //check if coin is valid
            if (validCoin(theCoin.Value))
            {
                availableCredit += theCoin.Value;       //update coins entered
                moneyRecieved.Add(theCoin);             //put this in the pile of coins we've made.
            }
            else
            {
                deliveryChute.Add(theCoin);             //send the coin back where it came from
            }
            

        }

        //adds pops to their chutes (to be dispensed)
        public void addPops(string popType, int popChute, int numPops)
        {
            if (pops.ContainsKey(popType))
            {
               
                for (int p=0; p< numPops;p++)  //adds the required number of pops to the specified section.
                {
                    chuteList[popChute].Enqueue(new Pop(popType));
                }

            }
            else
            {
                throw new Exception("this pop is not confugured for this machine");
            }
        }

        //adds the pop type to the machine. Used with configure (DOESNT ADD ACTUAL POPS)
        public void addPopTypes(List<string> newPops, List<int> popCosts) 
        {
            popKinds = new int[popCosts.Count];
            for (int p=0; p< newPops.Count; p++)
            {
                if (popCosts[p] > 0)
                {
                    pops.Add(newPops[p], popCosts[p]);
                    popKinds[p] = popCosts[p];
                }
                else
                {
                    throw new Exception("Pop cost can't be zero");
                }

            }
            //initialize number of types of pops and their chutes.
            chuteList = new Queue<Pop>[newPops.Count];
            int tempP = newPops.Count;
            for (int p=0; p<newPops.Count; p++)
            {
                chuteList[p] = new Queue<Pop>();
            }
            
        }

        //presses a button
        public void pressButton(int buttonNumber)
        {
            if (chuteList[buttonNumber].Count > 0)
            {
                int tempCost = popKinds[buttonNumber];      //get cost of pop from chute
                if (availableCredit > tempCost)
                {
                    availableCredit -= tempCost;               //update available credit because pop is delivered.
                    Pop tempPop = chuteList[buttonNumber].Dequeue();
                    deliveryChute.Add(tempPop);
                    getChange(availableCredit);
                }
                else
                {
                    //Console.WriteLine("Not enough available credit for this purpose");
                }
                
                

                

            }
            else
            {
                throw new Exception("This machine doesn't have this pop");
            }
        }
        
        

        //returns a list of pop and money from chute.
        public List<Deliverable> extractFromChute()
        {
            return deliveryChute;
        }

        public List<IList> teardownMachine()
        {
            List<IList> teardownList = new List<IList>()
            {
                new List<Coin>(),
                new List<Coin>(),
                new List<Pop>()
            };
            //first is amount from change chutes, then the pile of money, then the list of pops left over.
            //for (int q=0; q< coinChute.Length; q++)

            foreach (Queue<Coin> i in coinChute)
            {
                Coin[] newQueue = i.ToArray();

                foreach (Coin j in newQueue)
                {
                    Coin tempCoin = j;
                    teardownList[0].Add(tempCoin);
                }
            }
            coinChute = new Queue<Coin>[coinKinds.Length];

            //add pile of money
            teardownList[1] = moneyRecieved;

            //third is the list of pops left over.
            foreach (Queue<Pop> i in chuteList)
            {
                Pop[] newQueue = i.ToArray();

                foreach (Pop j in newQueue)
                {
                    Pop tempPop = j;
                    teardownList[2].Add(tempPop);
                }
            }
            chuteList = new Queue<Pop>[popKinds.Length];
            return teardownList;
        }

        public void getChange(int changeRequired)
        {
            //for each coin type
            for (int d=0; d<coinKinds.Length; d++)
            {
                
                while (coinKinds[paymentOrder[d]]<= changeRequired)      //while amount required > current coin, move one coin to the change list, remove coin, decrement change required.
                {
                    Coin tempCoin = coinChute[paymentOrder[d]].Dequeue();
                    changeRequired -= tempCoin.Value;               //Decrement payment required
                    deliveryChute.Add(tempCoin);
                }
            }            
        }
    }
}