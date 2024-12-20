// See https://aka.ms/new-console-template for more information
using RICADO.Omron;
using System.Diagnostics;
using ClassLibrary1;

Console.WriteLine("Hello, World!");

var s_cts = new CancellationTokenSource();

var plc = new OmronPLC(32, 45, enConnectionMethod.TCP, "192.168.10.45", timeout: 5000, retries: 5);

await plc.InitializeAsync(s_cts.Token);

//var result = await plc.ReadClockAsync(s_cts.Token);
//await plc.WriteWordAsync(1234, 0, enMemoryWordDataType.ExtendedMemory_Bank0, s_cts.Token);

var amount = 1000;
var intBenchmarks = new IntBenchmarks(amount+1);
var stopwatch = new Stopwatch();

Console.WriteLine("START ******");

for (int i = 0; i<amount; i++)
{
    stopwatch.Restart();
    var result = await plc.ReadWordsAsync(0, 500, enMemoryWordDataType.ExtendedMemory_Bank0, s_cts.Token);
    stopwatch.Stop();
    intBenchmarks.Add((int)stopwatch.ElapsedMilliseconds);
    //var val = result.Values[0];
    //Console.WriteLine(val);
}

Console.WriteLine($"FINISHED. Number of reads: {amount} || Max Time: {intBenchmarks.Max} || Min Time: {intBenchmarks.Min} || Avg Time: {intBenchmarks.Avg}");

while (false)
{
    var result = await plc.ReadWordAsync(0, enMemoryWordDataType.ExtendedMemory_Bank0, s_cts.Token);
    var val = result.Values[0];
    Console.WriteLine(val);
    Thread.Sleep(500);
}

Console.ReadLine();