# GraphQL vs. REST – Prototyp-Plattform zur Optimierung des Informationsaustauschs zwischen Entwicklern und Nutzern
Dieses Repository enthält den Prototype einer Plattform, der dazu verwendet wurde, einen Vergleich von REST und GraphQL durchzuführen. Der Prototype wurde in zwei Varianten umgesetzt, einmal mit REST und einmal mit GraphQL, um beide Technologien praktisch zu vergleichen.
## 🎯 Ziel des Projekts
Die Plattform unterstützt den strukturierten Austausch zwischen Entwicklern und Nutzern von Softwareprojekten. 
-	Entwickler können ihre Softwareprojekte auf der Plattform präsentieren. 
-	Nutzer können zu diesen Projekten Diskussionen, Ideen und Fehlerberichte erstellen.  
-	Beiträge lassen sich kommentieren und über ein Abstimmungssystem bewerten.
## 🏗️ Architektur
Beide Varianten nutzen die gleiche Datenbankstruktur und implementieren dieselben Features.
-	**Frontend (Client, REST & GraphQL):** Angular 18
-	**Backend (REST):** ASP.NET Core 8 mit Swagger/OpenAPI
-	**Backend (GraphQL):** ASP.NET Core 8 mit HotChocolate
-	**Datenbank:** SQLite
