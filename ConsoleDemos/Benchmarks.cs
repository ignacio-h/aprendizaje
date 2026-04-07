using BenchmarkDotNet.Running;
using Demos.Decimals;

BenchmarkRunner.Run<DecimalsBase>();
BenchmarkRunner.Run<DecimalsParams>();
BenchmarkRunner.Run<DecimalsVsDouble>();
BenchmarkRunner.Run<DecimalsVsDoubleList>();
