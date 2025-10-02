Krav:

The following features need to be implemented:

A user needs to be able to register an account (x)
A user needs to be able to log out. (x)
A user needs to be able to log in. (x)
A user needs to be able to upload information about the item they wish to trade.
A user needs to be able to browse a list of other users items.
A user needs to be able to request a trade for other users items.
A user needs to be able to browse trade requests.
A user needs to be able to accept a trade request.
A user needs to be able to deny a trade request.
A user needs to be able to browse completed requests.



README.md

Software is a assigment ment for c# programming at my school. 
All the input is support to be put in as the system request them.
This is not ment be a final version of the software, but rather a MVP to show knowledge of what I've learned.


Jag kände att jag får väl över gå till svenska för den här dela av dokumentationen.
rent ärligt känner jag att mellan att laddat på commits och kommenterat koden så vet jag inte riktigt vad jag ska skriva här.

klasser:

jag valde att använda User, Item, Trade, Datamanager.

jag hade IUser från början för att jag kopiera konceptet från school uppgiften men insåg att det var helt onödigt att ha för den bidrog inte med något så jag kunde lika bra köra på att bara ha en User klass.

User klassen är en helt vanlig användar klass. email, lösenord, användarnamn.
jag ville ha användarnamn för jag tyckte det var lättare när jag kode och det blev en snuttefilt när jag testa vissa funktioner så som att logga in, se att trades funka, och vem som äger ett item.
la även item listan i user klassen så att varje användare fick sin egen lista med items.
under user har jag även en metod för inloggning, en för att loop och kolla sina egna items och för att lägga till items i sin lista.

Under Item klassen har jag bara en konstruktor på hur item ska vara.

Trade klassen har en konstruktor på hur ett "trade meddelande" ska se ut. jag hade från början att item man skulle byta bytade listor men det blev så krångligt så jag valde att man bara kopierar namn på items och användare, för det i ett meddelande format och ger den enum pending som man sen kan använda. använder enums senare i menyn för att kunna använda som ett filter så att man kan se pending, accepted och denied. 

datamanager klassen. Rent ärligt känner jag inte att jag kan bli betyg satt på detta eller att den änns ska räknas med. tycker inte att vi fick något av genom gången på lektionen så jag valde att själv hitta en metod att göra detta. när jag googlade vad som skulle funka bra med klasser så var json att föredra(enligt internet) så jag tänkte why not. vi kör väl på det. mesta delan av koden är fram googlad och att jag har kollar hur andra har gjort på forum osv. så ja... jag kan inte säga att jag har skrivit denna kod själv, men det blir väl så också när lektion typ inte förklarade så mycket, och hela kod exemplet blev typ bara massa skämt och jag tappade total fokus när det inte kändes seriöst... så man gör det man kan och löser det själv.... anyway så hade jag det från början i program.cs men valde sen att lägga det i en egen klass och bygga metoder av det som jag kunde kalla på på olika ställen i programet. 

program.cs

i programmet köra jag ganska standad. skapa listor, ladda in från json, köra igången en while loop med ett enkelt inloggings system.
gjorde det vi gjorde i school projektet och hade en active user som var null och blev en user när man loggade in.
jag skrev först all kod i program cs och började sen flytta över och gjorde metoder av vissa av bitarna. 
jag känner mig dock osäker på hur jag skulle göra det med hela trading biten så jag lät den vara kvar.
traden ville jag hålla enkel. jag loopar fram alla användare som inte är active user och man får först välja vilken man vill byta med för att den välja vilket item. all den information sparas sen ner i en trade class som sparas i en egen lista. men en förinställd enum som är pending 

i "trade meddelande biten" får man en ny meny där man kan välja om man vill kolla pending, accepted eller denied.
i pending så loopas alla pending förfrågning fram genom att filtrera borta alla andra användare som inte är active user och filterar fram bara dom som har enum pending. och där kan man välja om man vill accept eller deny en trade. jag gjorde så att man kunde välja genom att första välja ett list "nr" för att sen svara ja eller nej. det fake nr är fullkodat men det funkar utan att man tänker på det i själva systemet. 

accept och deny är egentligen samma loop men utan att kunna göra några val. 

jag antar att det är det hela i systemet. 