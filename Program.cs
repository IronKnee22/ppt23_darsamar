bool konec = false;
bool pokracovat = true;
Random random = new Random();

while (!konec)
{
	int nahodneneCislo = random.Next(1, 101);
	while (pokracovat)
	{
		Console.Write("Myslím si číslo zkus ho uhádnout: ");
		string? input = Console.ReadLine();

		bool cislo = int.TryParse(input, out int hadaneCislo);

		if (cislo)
		{
			string VetsiMensi = hadaneCislo > nahodneneCislo ? "větší" : "menší";

			Console.WriteLine($"Tvoje číslo je {VetsiMensi}");

			if (hadaneCislo == nahodneneCislo)
			{
				Console.WriteLine("Vyhrál jsi");

				Console.WriteLine("Chceš pokračovat? ");
				string? a = Console.ReadLine();

				if (a?.ToLower() == "ano") { pokracovat = true; break; }
				else { pokracovat = false; konec = true; break; }
			}
		}
		else
		{
			if(input == "konec"){ pokracovat = false; konec = true; break; }
			else { Console.WriteLine("Zkus zadat číslo"); }
		}
	}


}
