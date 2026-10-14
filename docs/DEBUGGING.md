# Felsökningsrapport
### Ogiltig inmatning av antal deltagare

---

## 1. Undersökt fel

Jag undersökte ett fel i BookingConsole där programmet kraschade när användaren skrev bokstäver istället för siffror vid inmatningen av participants.

---

## 2. Återskapa felet

Jag startade programmet och försökte skapa en bokning. Vid participants matade jag in bokstäver istället för siffror.

Jag hade först missuppfattat delen om att återskapa felet. Därför ändrade jag tillbaka till den ursprungliga felaktiga koden och gjorde en separat commit:

`Reproduce invalid participants input bug`

---

## 3. Faktiskt resultat

Programmet kraschade vid felaktig inmatning och det gick inte att fortsätta med bokningen.

---

## 4. Förväntat resultat

Jag förväntade mig ett tydligt felmeddelande och att användaren skulle få försöka igen utan att börja om.

---

## 5. Felaktig kod

Felet fanns i CreateBooking() i BookingConsole.

```csharp
var participants = int.Parse(Console.ReadLine() ?? string.Empty);
```

Koden fungerade med siffror men kunde inte hantera bokstäver.

---

## 6. Orsak till felet

Console.ReadLine() returnerar en string som int.Parse() försöker omvandla till ett heltal.

Om användaren skriver bokstäver går det inte att omvandla texten. Det leder till en FormatException som inte hanterades, vilket fick programmet att krascha.

---

## 7. Lösning

Först ersatte jag int.Parse() med int.TryParse() i en if-sats.

```csharp
if (!int.TryParse(Console.ReadLine(), out int participants))
{
    Console.WriteLine("Enter a valid number.");
    return;
}
```

Det förhindrade kraschen, men användaren behövde börja om med bokningen eftersom metoden avslutades med return.

Jag var inte nöjd med det eftersom namn, rum, datum och tider redan hade fyllts i.

Därför valde jag en while-loop istället.

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

Nu får användaren försöka igen tills ett giltigt heltal matas in.

Till sist flyttade jag loopen från CreateBooking() till en egen metod, ReadValidParticipants(), för att enklare kunna testa just inmatningen.

---

## 8. Verifiering

Jag testade programmet med både bokstäver och siffror.

Vid felaktig inmatning visades ett felmeddelande och jag kunde försöka igen. Med ett giltigt antal deltagare kunde bokningen registreras.

Jag körde även de befintliga testerna med den felaktiga koden. Alla passerade trots att programmet kraschade.

Då misstänkte jag att tester saknades för konsolinmatningen.

---

## 9. Automatiserade tester

Jag tog hjälp av AI för att skriva två tester i BookingConsoleTests.

Det första, ReadValidParticipants_ValidInput_ReturnsNumber, kontrollerar att siffran 4 accepteras och returneras.

Det andra, ReadValidParticipants_InvalidInput_AllowsRetry, matar först in bokstäver och sedan 4. Testet kontrollerar att metoden låter användaren försöka igen utan att krascha.

För att säkerställa att testet upptäckte felet ändrade jag tillfälligt tillbaka metoden till den gamla koden.

```csharp
var participants = int.Parse(Console.ReadLine() ?? string.Empty);
return participants;
```

Då misslyckades testet med bokstäver, medan testet med giltig inmatning fortfarande fungerade.

Det visade att testet kunde upptäcka den ursprungliga buggen.