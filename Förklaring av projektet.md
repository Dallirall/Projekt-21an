Projekt 21an är ett enkelt textbaserat spel, där man spelar 21an (Black Jack) mot datorn. Spelaren och datorn får ett antal slumpade kort, dragna ur en virtuell kortlek. Spelaren får därefter välja att dra fler kort tills hen är nöjd, varpå datorn börjar dra kort. Under spelets gång checkas poängställningarna av via separata metoder för att se om spelet avgjorts. 

I en startmeny till spelet kan man välja en av fyra svårighetsgrader, samt justera olika inställningar.

Spelet är färdigt och publicerat till en exe-fil som kan köras på Windows-system. Det finns dock planer på att vidareutveckla spelet.


Tekniker jag har använt mig av:

Objektorientering
I den grad jag behärskar har jag strävat efter att programmera objektorienterat, och försökt hålla en DRY-approach som möjliggör återanvändning av kod. Klasser är ordnade i separata klassfiler och vid behov uppdelade i partial classes. 

Inheritance och polymorphism
För att göra en virtuell kortlek skapade jag en klass där varje kort är ett objekt, och kortleken en lista av dessa objekt. Kortleksklassen ville jag göra på engelska för att kunna återanvända, men i 21anspelet (som är på svenska) har jag en klass som ärver från kortleksklassen, och overridar de metoder där till exempel kortets namn (ex. “Ruter Ess”) ska vara på svenska.

Databaser i SQLite
Jag har använt mig av en SQLite databastabell för att logga vinsthistoriken. Nya spelare läggs till i tabellen, och vinster, förluster och oavgjorda matcher uppdateras efter spelomgången. Jag använde Dapper för att utföra SQL queries i programmet. 

Interfaces
Ett exempel på när jag använt interfaces är i ett experiment med cross-plattformkompabilitet till spelet. Jag gjorde ett interface för olika plattformar. Varje plattform är en separat klass som implementerar interfacets metoder med filsökvägar specifika för respektive OS (genom bl.a. Environment.SpecialFolder). När en filsökväg behövs i programmet går det via en variabel av interfacets typ, för att komma åt den aktuella plattformens metoder på ett enkelt sätt. Jag planerar att vidareutveckla detta i ett nästa steg.  

Enums
Används för konstanter i koden. T.ex är valörerna och sviterna på korten enumvärden. 

Linq
Använder mig av enstaka Linq-metoder, men behärskar på en grundläggande nivå även metoder som Where, Select, Any mm. 

Git
Committar ändringar regelbundet och pushar till min GitHub, främst via Visual Studio, men ibland utför jag också Git commands i Git Bash.
