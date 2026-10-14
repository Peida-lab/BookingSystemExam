# Felsökningsrapport
### Ogiltig inmatning av antal deltagare

---

## 1. Undersökt fel

Jag undersökte felet där programmet kraschade när användaren skrev in bokstäver istället för siffror, alltså fel inmatning vid antal deltagare i BookingConsole.

---

## 2. Återskapa felet

Jag körde programmet och försökte skapa en bokning.

När det kom till participants matade jag in bokstäver istället för siffror.

Jag hade först uppfattat uppgiften lite fel när det gällde att återskapa felet, så jag gick tillbaka och ändrade den korrekta koden till den ursprungliga koden som fick programmet att krascha.

Jag gjorde även en separat commit för den delen med namnet:

`Reproduce invalid participants input bug`

---

## 3. Faktiskt resultat

Det resulterade i att programmet kraschade och man kunde inte fortsätta med bokningen.

---

## 4. Förväntat resultat

Jag hade förväntat mig att programmet skulle visa ett tydligt felmeddelande och be mig skriva in antalet participants igen.

---

## 5. Felaktig kod

Felet fanns i CreateBooking() i BookingConsole.

```csharp
var participants = int.Parse(Console.ReadLine() ?? string.Empty);
```

Den fungerade så länge man matade in rätt värde, alltså siffror, men den kunde inte hantera bokstäver.

---

## 6. Orsak till felet

Console.ReadLine() returnerar en string som värde och int.Parse() försöker omvandla texten till ett heltal.

När användaren skriver bokstäver kan texten inte omvandlas till ett tal.

Det leder till en FormatException och eftersom undantaget inte hanteras kraschar programmet och sen avslutas.

---

## 7. Lösning

Jag ändrade först den befintliga koden till en vanlig if-sats med int.TryParse().

```csharp
if (!int.TryParse(Console.ReadLine(), out int participants))
{
    Console.WriteLine("Enter a valid number.");
    return;
}
```

Om användaren skrev in ett felaktigt värde visades felmeddelandet Enter a valid number och metoden avslutades med return.

Sen var jag inte nöjd med att man först fyllde i namn, rumsnamn, datum och tider, och om man till sist råkade skriva en bokstav behövde man gå tillbaka till början och registrera hela bokningen igen.

Så jag lade till en while-loop istället.

För att det skulle fungera behövde jag först skapa variabeln participants utanför while-loopen med int participants.

Jag ändrade sedan koden till:

```csharp
int participants;

while (true)
{
    Console.Write("Participants: ");

    if (int.TryParse(Console.ReadLine(), out participants))
        break;
    else
    {
        Console.WriteLine("Enter a valid number.");
        continue;
    }
}
```

Nu kontrollerar int.TryParse() om användaren har skrivit ett giltigt heltal.

Om inmatningen är felaktig visas meddelandet Enter a valid number och loopen fortsätter med continue.

När användaren skriver ett giltigt heltal avslutas loopen med break och programmet fortsätter med bokningen.

---

## 8. Verifiering

Jag startade programmet och försökte skapa en bokning.

När det kom till att mata in participants testade jag med olika inmatningar flera gånger för att kontrollera att while-loopen fungerade.

Vid felaktig inmatning fick jag ett felmeddelande och kunde försöka igen.

Min bokning registrerades när jag matade in rätt värde, alltså siffror.

Jag körde även alla befintliga tester när jag hade återskapat felet.

Alla tester lyckades trots att programmet fortfarande kraschade när jag skrev bokstäver istället för siffror.

Det var då jag började misstänka att det inte fanns några tester för just den delen av programmet.

---

## 9. Automatiserade tester

Just den delen av BookingConsole hade inga automatiserade tester.

De befintliga testerna i BookingServiceTests kontrollerar bland annat rummens kapacitet, men inte själva inmatningen av participants.

Jag funderar därför på att skriva några tester själv i BookingConsoleTests för att kontrollera att programmet hanterar felaktig inmatning och låter användaren försöka igen.