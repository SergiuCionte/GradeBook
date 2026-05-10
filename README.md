# Principiile SOLID: Unde au fost incalcate?

**1. Single Responsability Principle**
    Controller face prea multe lucruri in fisierul ItemController (si rutare, si calcule (de exemplu calcularea average-ului in GetAll, actiune ce ar trebui sa fie realizata de service, si log-uri in consola, atat in GetAll cat si in GetById, pe care le mutam ulterior in Service).

    
  Rezolvare: Facem ca si controller sa primeasca cereri HTTP si sa trimita raspunsul, iar calculele sa fie realizate intr-un ItemService, iar pentru a respecta Dependency Inversion Principle, vom face ca si controller-ul sa depinda de abstractizare, nu de clase mai low-level, adica folosim interfete, cum ar fi IItemService si ILogger.
             Loggerele ce tin de logica vor fi mutate in service folosind ILogger.
  
**2. Open-Closed Principle**
    In fisierul ItemRepository, metoda GetAllAsync nu era bine deoarece nu era disponibila pentru extensie pentru ca daca cineva ar fi vrut sa vada toti itemii (inclusiv cei cu IsActive = false), ar fi trebuit sa intre in codul repository-ului si sa il modifice acolo.

    
  Rezolvare: Vom separa in 2 metode, una GetAllAsync noua, care vedem toti itemii indiferent de isActive si una QueryAsync prin care vom putea vedea toti itemii pe baza unui filtru, de aici si separarea interfetelor in IItemQuery si IItemReader, iar IItemReader poate fi folosita in cazul in care se doreste vizualizarea datelor brute, nefiltrate.
  
**3. Interface Segregation**
    Este evitata pe viitor datorita separarii intre IItemQuery si IItemReader in caz ca un alt potential service ar avea nevoie doar de date brute de exemplu, fara date filtrate.

 
**4. Dependency Inversion**
    Dupa ce am rezolvat sa functioneze ItemRepository sa ia date de la url specificat si un ItemService care sa proceseze aceste date in modul corespunzator, trebuie sa ne asiguram ca nu mai exista legatura intre controller si repo, ci doar cu service prin interfata ca sa asiguram dependency inversion principle, adica nivelul superior(ItemController) sa nu depinda de nivelul inferior(ItemService) decat prin interfete(IItemService), analog si pentru Service si Repository, conectate doar prin IItemReader .

**Alte modificari**
S-a adaugat ItemResponse, in models care este un model de deserializare, reprezintă structura JSON-ului returnat de API-ul extern. Link-ul pentru preluarea datelor din URL, il tinem in appsettings.json
