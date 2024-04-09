using System;

namespace Paralelizmus {
	
	internal class Program {

		static bool canPop(int chance) {
			Random RandomGen = new Random();
			int randomValueBetween0And99 = RandomGen.Next(100);
			return randomValueBetween0And99 < chance;
		}
		
		static async Task Main(string[] args) {
			// Threads();
			// Sync();
			// await Asyncronni();
			await Experiment();
		}

		static async Task Asyncronni() {
			Console.WriteLine("a");
			Task task = Task.Delay(1000);
			Console.WriteLine("B");
			await task;
			Console.WriteLine("endeee");


			int x = await ReadNUm();
			
			Console.ReadLine();
		}


		static Task<int> GetNumCorrect() { // correct
			return Task.FromResult(1);
		}
		
		
		static async Task<int> GetNumIncorrect() { // correct
			return 1;
		}

		static async Task Experiment() {
			Console.WriteLine("Start");

			await Task.Delay(1000);

			using StreamWriter sw = new StreamWriter("text.txt");
			await sw.WriteLineAsync("asdasdasdasd");

			await Task.Delay(1000);

			Console.WriteLine("END");
		}
		
		static async Task<int> ReadNUm() {
			string txt = await File.ReadAllTextAsync("text.txt");
			return int.Parse(txt);
		}
		
		static void Sync() {
			SimpleStack<int> stack = new SimpleStack<int>();
			Random RandomGen = new Random();
			
			object lockObject = new object();

			Thread newThread = new Thread(() => {
				while (true) {
					lock (lockObject) {
						stack.Push(RandomGen.Next(100));
						Monitor.Pulse(lockObject);
					}
					Thread.Sleep(100);	

				}
			});
			newThread.Start();

			
			for (int i = 0; i < 5; i++) {
				Thread t = new Thread(() => {
					while (true) {
						int? x = null;
						lock (lockObject) {
							if (!stack.IsEmpty) {
								x = stack.Pop();
							}
							else {
								Monitor.Wait(lockObject);
							}
						}

						if (x != null){
							Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}: {x}");
						}
						else {
							Console.WriteLine("empty");
						}
						
						Thread.Sleep(RandomGen.Next(
							minValue: 40,
							maxValue: 400
						));
					}
				});
				t.Start();
			}
		}
		
		
		static void Threads() {
			SimpleStack<int> stack = new SimpleStack<int>();
			Random RandomGen = new Random();
			object lockObj = new object();
			// current tid
			int tid = Thread.CurrentThread.ManagedThreadId;
			
			for (int i = 0; i < 5; i++) {
				Thread thread = new Thread(() => {
					while (true) {
						if (!canPop(60)) {
							stack.Push(RandomGen.Next(100));
							continue;
						}

						lock (lockObj) {
							if (!stack.IsEmpty) {
								int x = stack.Pop();
							}
						}
					}
				});
				thread.Start();
			}

			while (true) {
				Console.WriteLine("B");
			}
		}
	}
}