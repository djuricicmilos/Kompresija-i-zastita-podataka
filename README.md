# Kompresija i zaštita podataka - Projektni zadaci

Ovaj repozitorijum sadrži implementaciju algoritama za kompresiju i zaštitu (kodovanje) podataka u okviru kursa Kompresija i zaštita podataka. Projekat je realizovan u **.NET** okruženju koristeći **C#**.

## 📋 Pregled zadataka

### Zadatak 1: Algoritmi kompresije
Implementacija osnovnih metoda za analizu i redukciju redundansi u podacima:
* **Analiza Entropije:** Izračunavanje bajt-entropije ulaznog fajla na osnovu verovatnoće pojavljivanja svakog od 256 mogućih bajtova.
* **Statistički Kodovi:**
    * **Shannon-Fano:** Konstrukcija koda i kompresija.
    * **Huffman:** Optimalno prefiksno kodiranje.
* **Rečnički Algoritmi:**
    * **LZ77:** Klizni prozor (Sliding window) pristup.
    * **LZW:** Dinamičko građenje rečnika.
* **Optimizacija:** Podaci se zapisuju na bajt-nivou kako bi se postigao asimptotski optimalan stepen kompresije. Implementiran je i proces dekodiranja za svaki algoritam radi validacije (lossless compression).

### Zadatak 2: LDPC kodovi
Implementacija algoritama za detekciju i korekciju grešaka:
* **Konstrukcija H matrice:** Generisanje regularne LDPC matrice sa parametrima $n=15, n-k=9, w_r=5, w_c=3$ koristeći fiksni seed (broj indeksa).
* **Sindromsko dekodiranje:** Generisanje tabele sindroma i korektora, kao i određivanje minimalnog kodnog rastojanja koda.
* **Gallager B algoritam:** Implementacija iterativnog algoritma dekodiranja (bit-flipping).
* **Analiza greške:** Pronalaženje minimalne težine greške $e$ koju Gallager B ne može da ispravi sa pragovima $th_0 = th_1 = 0.5$.

---

## Tehnologije i pokretanje

### Preduslovi
* [.NET SDK](https://dotnet.microsoft.com/download) (verzija 6.0, 7.0 ili 8.0)

### Instalacija i pokretanje
1.  **Klonirajte repozitorijum:**
    ```bash
    git clone https://github.com/djuricicmilos/Kompresija-i-zastita-podataka
    cd Kompresija-i-zastita-podataka
    ```

2.  **Restauracija biblioteka:**
    ```bash
    dotnet restore
    ```

3.  **Pokretanje aplikacije:**
    Projekat je konfigurisan kao konzolna aplikacija. Možete ga pokrenuti komandom:
    ```bash
    dotnet run --project PutanjaDoProjekta/Projekat_1.csproj
    dotnet run --project PutanjaDoProjekta/Projekat_2.csproj
    ```

---
