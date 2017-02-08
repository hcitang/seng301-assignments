﻿using System.IO;
using System;
using Frontend1;
using Frontend1.Parser;

namespace seng301_asgn1
{
    public class ScriptProcessor
    {
        VendingMachineParser parser;
        SENG301VMAnalyzer analyzer;

        public ScriptProcessor(TextReader reader, IVendingMachineFactory factory)
        {
            this.analyzer = new SENG301VMAnalyzer();
            this.analyzer.RegisterVendingMachineFactory(factory);
            this.parser = new VendingMachineParser(reader, this.analyzer);
        }

        public ScriptProcessor(string pathToScript, IVendingMachineFactory factory) :
            this(new StreamReader(File.OpenRead(pathToScript)), factory)
        {
        }

        public void Parse()
        {
            this.parser.Parse();
        }

        public static void Main(string[] args)
        {
            /*List<int> coinKinds = new List<int>();
            coinKinds.Add(5);
            coinKinds.Add(25);
            //[5, 10, 25, 50];

            System.Console.WriteLine(coinKinds[0]);
            System.Console.WriteLine(coinKinds[1]);
            VendingMachine thisMachine = new VendingMachine(coinKinds, 3);
            System.Console.WriteLine(thisMachine.validCoin(25));
            System.Console.WriteLine(thisMachine.validCoin(5));
            System.Console.WriteLine(thisMachine.validCoin(23));
            //List<Coin> testDelivery = */


            
            var goodScripts = Directory.GetFiles("test-scripts", "T*");
            foreach (var script in goodScripts) {
                Console.WriteLine(script + ":");
                var scriptParser = new ScriptProcessor(script, new VendingMachineFactory());
                scriptParser.Parse();
            }
            var badScripts = Directory.GetFiles("test-scripts", "U*");
            foreach (var script in badScripts) {
                Console.WriteLine(script + ":");
                try {
                    var scriptParser = new ScriptProcessor(script, new VendingMachineFactory());
                    scriptParser.Parse();
                }
                catch {
                    Console.WriteLine("..FAIL (which is good)");
                }
            }
            

        }
    }
}