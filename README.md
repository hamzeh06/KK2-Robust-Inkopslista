# Robust inköpslista – Kunskapskontroll 2

## Del 1 – Felsökning

Jag har gått igenom startkoden och rättat fel som gjorde att programmet kunde krascha eller visa fel resultat.

### Fel 1 – Programmet kraschade vid inläsning

**Vad hände?** Programmet kraschade när det fanns en tom rad i `items.txt`.

**Varför?** Programmet försökte läsa information från en tom rad som om den innehöll en vara.

**Lösning:** Jag lade till en kontroll som hoppar över tomma rader.

### Fel 2 – Programmet kraschade om filen saknades

**Vad hände?** Programmet kunde inte starta utan `items.txt`.

**Varför?** Det försökte läsa en fil som inte fanns.

**Lösning:** Jag använde `File.Exists()` för att kontrollera om filen finns. Om den saknas startar programmet med en tom lista.

### Fel 3 – Felaktig inmatning kunde krascha programmet

**Vad hände?** Om användaren skrev bokstäver i menyn, som pris eller som varunummer kunde programmet krascha.

**Varför?** Koden använde `int.Parse()`, som inte kan omvandla vanlig text till ett heltal.

**Lösning:** Jag bytte till `int.TryParse()` och visar ett felmeddelande om användaren skriver fel.

### Fel 4 – Fel varunummer kunde krascha programmet

**Vad hände?** Programmet kunde krascha om användaren försökte ta bort en vara med ett nummer som inte fanns.

**Varför?** `RemoveAt()` försökte ta bort en plats utanför listan.

**Lösning:** Jag lade till en kontroll som ser till att numret finns innan varan tas bort.

### Fel 5 – Totalsumman blev fel

**Vad hände?** Programmet räknade inte med den första varans pris.

**Varför?** Loopen i `Total()` började på index 1 i stället för 0.

**Lösning:** Jag ändrade loopen så att den börjar på 0 och räknar med alla varor.

### Fel 6 – Programmet kunde visa att listan sparats trots fel

**Vad hände?** Programmet kunde skriva att listan var sparad även om sparningen misslyckades.

**Varför?** Det fanns en tom `catch` som dolde felet. Meddelandet om lyckad sparning visades ändå.

**Lösning:** Jag lade meddelandet om lyckad sparning inuti `try` och använde särskilda `catch` för `IOException` och `UnauthorizedAccessException`.

Jag har också förbättrat sökningen så att den fungerar med stora och små bokstäver.

## Del 2 – Validering och budget

Jag ändrade konstruktorn i `Item` så att ett tomt namn ger `ArgumentException` och ett negativt pris ger `ArgumentOutOfRangeException`.

Jag lade också till ett budgettak på 500 kr i `ShoppingList`.

### Mitt designval för Add()

Jag valde att låta `Add()` returnera `bool`.

Om varan får plats inom budgeten läggs den till och metoden returnerar `true`. Om budgeten skulle överskridas returnerar den `false` och varan läggs inte till.

Jag valde detta eftersom det är enkelt att kontrollera resultatet i `Program.cs` och visa ett tydligt meddelande för användaren.

## Klassdiagram

```text
+---------------------------+
|          Program          |
+---------------------------+
| Sköter menyn              |
| Läser användarens val     |
| Hanterar felmeddelanden   |
+-------------+-------------+
              |
              v
+---------------------------+
|        ShoppingList       |
+---------------------------+
| items: List<Item>         |
| path: string              |
| maxBudget: int            |
+---------------------------+
| Add()                     |
| RemoveAt()                |
| Total()                   |
| Find()                    |
| Print()                   |
| Save()                    |
| Load()                    |
+-------------+-------------+
              |
              v
+---------------------------+
|            Item           |
+---------------------------+
| Name: string              |
| Price: int                |
+---------------------------+
| Item()                    |
| ToString()                |
+---------------------------+
```

`Program` använder `ShoppingList` för att hantera inköpslistan. `ShoppingList` innehåller flera objekt av klassen `Item`. Varje `Item` har ett namn och ett pris.

## Testning

Jag har testat felaktiga menyval, ogiltiga priser, felaktiga varunummer, sökning, totalsumma och att starta utan sparad fil.

Jag har också testat att tomma namn och negativa priser stoppas och att en vara inte kan läggas till om budgeten överskrids.