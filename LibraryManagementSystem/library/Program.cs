using System.Globalization;

List<string> kitablist = new List<string>();
List<string> verilenlist = new List<string>();
List<string> zamanlist = new List<string>();
List<string> musteriad = new List<string>();

string kitabdata = "kitabdata.txt";
string verilendata = "verilendata.txt";
string zamandata = "zamandata.txt";
string musteridata = "musteridata.txt";

if (File.Exists(kitabdata))
{
    kitablist.AddRange(File.ReadAllLines(kitabdata));
}

if (File.Exists(verilendata))
{
    verilenlist.AddRange(File.ReadAllLines(verilendata));
}

if (File.Exists(zamandata) && File.Exists(musteridata))
{
    string[] zamanlarim = File.ReadAllLines(zamandata);
    string[] musterilerim = File.ReadAllLines(musteridata);

     int say2 = Math.Min(zamanlarim.Length, musterilerim.Length);

    for (int i = 0; i <say2; i++)
    {
        zamanlist.Add(zamanlarim[i]);
        musteriad.Add(musterilerim[i]);
    }
}

///////////////Kitablarim Systems////////////////////////

void butunkitablarim()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Kitabxananın bütün kitabları:");
    if (kitablist.Count == 0)
    {
        Console.WriteLine("Kitabxanada kitab yoxdur!");
        return;
    }
    for (int i = 0; i < kitablist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{kitablist[i]}");
    }
}

void VerilenKitablarim()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Müşteriye verilen kitablar:");
    if (verilenlist.Count == 0)
    {
        Console.WriteLine("Müşteriye verilen kitab yoxdur!");
        return;
    }
    Console.WriteLine("ID-KITAB-MÜŞTERİ-QAYTARILACAQ ZAMAN:");
    for (int i = 0; i < verilenlist.Count; i++)
    {
        Console.WriteLine($"{i + 1}-{verilenlist[i]}-{musteriad[i]}-{zamanlist[i]}");
    }
}

void VarolanKitablarim()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Kitabxanada hal-hazırda varolan kitablar:");
    if (verilenlist.Count == 0)
    {
        butunkitablarim();
        return;
    }
    int sayac = 0;
    for (int i = 0; i < kitablist.Count; i++)
    {
        if (!verilenlist.Contains(kitablist[i]))
            {
                sayac += 1;
                Console.WriteLine($"{sayac}-{kitablist[i]}");
            }
    }
}

/////////////////Ver-Alma Systems/////////////////////
void Kitabelaveet()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Kitabxanaya kitab elave et");
    while (true)
    {
    Console.Write("Kitab Adı: ");
    string kitabadi = Console.ReadLine();
        if (kitabadi.Length > 2)
        {
            kitablist.Add(kitabadi);
            File.WriteAllLines(kitabdata, kitablist);
            Console.WriteLine("Uğurnan elave olundu!");
            break;
        }
        else
        {
            Console.WriteLine("En az 3 herf olmalıdır!");
        }
    }

}

void Kitabver()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Müşteriye kitab ver");
    int kitabindex = 0;
while (true)
{
    var musaitKitablar = kitablist.Where(k => !verilenlist.Contains(k)).ToList();

    Console.WriteLine("Mövcud kitablar:");
    for (int i = 0; i < musaitKitablar.Count; i++)
    {
        Console.WriteLine($"{i + 1} - {musaitKitablar[i]}");
    }

    Console.Write("Müşteriye vermek istediyiniz kitab id: ");
    int secim;
    if (int.TryParse(Console.ReadLine(), out secim) &&
        secim > 0 && secim <= musaitKitablar.Count)
    {
        verilenlist.Add(musaitKitablar[secim - 1]);
        break;
    }
    else
    {
        Console.WriteLine("Düzgün ID girmədiniz!");
    }
}
    while (true)
    {
        Console.Write("Müşterinin adı ve soyadı(istəyə bağlı): ");
        string musteriadstring = Console.ReadLine();
        if (musteriadstring.Length > 2)
        {
            musteriad.Add(musteriadstring);
            break;
        }
        else
        {
            Console.WriteLine("En az 3 herfli olmalıdır.");
        }
    }
    while (true)
    {
        Console.Write("Kitab hansi tarixde qaytarilacaq?(NÜM: 30.12.2025): ");
        string date = Console.ReadLine();

        DateTime tarix;
    if (!string.IsNullOrWhiteSpace(date) && DateTime.TryParseExact(date, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out tarix))
    {
        Console.WriteLine("Daxil etdiyiniz tarix: " + tarix.ToString("dd.MM.yyyy"));
        zamanlist.Add(date);
        break;
     }
    else
    {
            Console.WriteLine("Tarix düzgün formatda deyil!");
    }
    }
    File.WriteAllLines(zamandata, zamanlist);
    File.WriteAllLines(musteridata, musteriad);
    File.WriteAllLines(verilendata, verilenlist);
    Console.WriteLine("Kitab uğurla verildi!");
}

void kitabal()
{
    Console.WriteLine("-----------------------");
    Console.WriteLine("Müşteriden kitab götürme sistemi");
    VerilenKitablarim();
    Console.WriteLine("-----------------------");
    Console.WriteLine("Götürdüyünüz kitabın ID-sini girin");
    while (true)
    {
        Console.Write("ID:");
        int kitabalmaid = Convert.ToInt32(Console.ReadLine());
if (kitabalmaid > 0 && kitabalmaid <= verilenlist.Count)
        {
            verilenlist.RemoveAt(kitabalmaid - 1);
            File.WriteAllLines(verilendata, verilenlist);

            zamanlist.RemoveAt(kitabalmaid - 1);
            File.WriteAllLines(zamandata, zamanlist);

            musteriad.RemoveAt(kitabalmaid - 1);
            File.WriteAllLines(musteridata, musteriad);

            Console.WriteLine("Uğurnan silindi!");

            break;
        }
        else
        {
            Console.WriteLine("Sehv ID girişi!");
         }
    }

}



/////////////////MENU/////////////////////////
do
{
    Console.WriteLine("=======================");
    Console.WriteLine("1-Kitablar Menusu \n2-Kitab Elave Et \n3-Kitab Ver \n4-Kitab Al \n5-Cıxış");
    Console.WriteLine("=======================");
    Console.Write("ID: ");
    int id = Convert.ToInt32(Console.ReadLine());
    if (id > 0 && id < 6)
    {
        if (id == 1)
        {
            kitabmenu();
        }
        else if (id == 2)
        {
            Kitabelaveet();
        }
        else if (id == 3)
        {
            Kitabver();
        }
        else if (id == 4)
        {
            kitabal();
        }
        else
        {
            break;
        }
    }

} while (true);

void kitabmenu()
{
    while (true)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine("1-Kitablarim \n2-Varolan Kitablarim \n3-Verilen Kitablarim \n4-Ana Menu");
        Console.Write("ID: ");
        int kitabid = Convert.ToInt32(Console.ReadLine());
        if (kitabid > 0 && kitabid < 5)
        {
            if (kitabid == 1)
            {
                butunkitablarim();
            }
            else if (kitabid == 2)
            {
                VarolanKitablarim();
            }
            else if (kitabid == 3)
            {
                VerilenKitablarim();
            }
            else if (kitabid == 4)
            {
                break;
            }
        }
        else
        {
            Console.WriteLine("Sehv ID!");
        }
    }
}