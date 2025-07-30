# KWHotelDecryptor

Narzędzie do odszyfrowywania plików konfiguracyjnych systemu KWHotel (KajWare Hotel Management System).

## Opis

KWHotelDecryptor to aplikacja konsolowa napisana w C#, która pozwala na odszyfrowanie i wyświetlenie 
zawartości plików konfiguracyjnych `connect.config` używanych przez system zarządzania hotelami KWHotel 
firmy KajWare. 

Program automatycznie wyszukuje instalacje KWHotel na systemie lub pozwala na ręczne podanie 
ścieżki do pliku konfiguracyjnego.

## Funkcjonalności

- **Automatyczne wyszukiwanie** - Program automatycznie przeszukuje standardowe lokalizacje instalacji KWHotel
- **Ręczne podawanie ścieżki** - Możliwość podania konkretnej ścieżki do pliku `connect.config`
- **Obsługa przeciągania plików** - Obsługa drag & drop plików konfiguracyjnych
- **Deszyfrowanie DES** - Wykorzystanie algorytmu DES do odszyfrowania plików konfiguracyjnych
- **Czytelne wyświetlanie** - Formatowanie i wyświetlanie informacji o konfiguracji bazy danych

## Wymagania systemowe

- .NET Framework 4.7.2 lub nowszy
- System operacyjny Windows
- Uprawnienia do odczytu plików konfiguracyjnych KWHotel

## Kompilacja

Projekt można skompilować za pomocą Visual Studio lub MSBuild:

```bash
msbuild KWHotelDecryptor.sln /p:Configuration=Release
```

## Użycie

### Automatyczne wyszukiwanie

Uruchom program bez argumentów - automatycznie przeszuka standardowe lokalizacje:

```bash
KWHotelDecryptor.exe
```

Program przeszuka katalog `C:\Kajware\` w poszukiwaniu instalacji KWHotel.

### Ręczne podanie ścieżki

Podaj ścieżkę do pliku `connect.config` jako argument:

```bash
KWHotelDecryptor.exe "C:\KajWare\KWHotel Pro\connect.config"
```

### Przeciągnij i upuść

Przeciągnij plik `connect.config` na plik wykonywalny programu.

## Struktura projektu

```
KWHotelDecryptor/
├── Consts.cs                 # Stałe kryptograficzne (klucz i IV)
├── KajwareCrypto.cs          # Klasa obsługująca szyfrowanie/deszyfrowanie DES
├── KajwareConfigParser.cs    # Parser plików konfiguracyjnych
├── KwHotelConfig.cs          # Model danych konfiguracji
├── Program.cs                # Główna logika aplikacji
├── KWHotelDecryptor.csproj   # Plik projektu
└── KWHotelDecryptor.sln      # Plik rozwiązania
```

## Informacje wyświetlane

Program wyświetla następujące informacje z pliku konfiguracyjnego:

- **Serwer baz danych** - Typ serwera bazy danych
- **Adres serwera** - Adres IP lub nazwa hosta
- **Port serwera** - Port połączenia (lub "brak (domyślny)")
- **Użytkownik** - Nazwa użytkownika bazy danych
- **Hasło** - Hasło do bazy danych
- **Baza danych** - Nazwa bazy danych
- **SSL** - Status połączenia SSL (włączone/wyłączone)

## Obsługa błędów

Program obsługuje następujące sytuacje błędne:

- Brak uprawnień do odczytu pliku
- Nieistniejące pliki konfiguracyjne
- Błędy deszyfrowania
- Brak instalacji KWHotel w standardowych lokalizacjach

## Bezpieczeństwo

⚠️ **Ostrzeżenie**: Program wyświetla w konsoli dane logowania do bazy danych, w tym hasła w postaci jawnej. 
Używaj go tylko w bezpiecznym środowisku i upewnij się, że dane wyjściowe nie są zapisywane w logach systemowych.

## Szczegóły techniczne

### Algorytm szyfrowania

- **Algorytm**: DES (Data Encryption Standard)
- **Klucz**: Pierwsze 8 bajtów z łańcucha "!5663b#KN" (kodowanie UTF-8)
- **IV (wektor inicjalizujący)**: [38, 55, 206, 48, 28, 64, 20, 16]
- **Format danych**: Base64 → DES → UTF-8

### Format pliku konfiguracyjnego

Odszyfrowany plik zawiera dane w formacie tekstowym:
```
[typ_serwera_bazy]
[host],[port]
[ssl_enabled]
[użytkownik]
[hasło]
[nazwa_bazy]
```

## Licencja

Ten projekt został stworzony w celach edukacyjnych i diagnostycznych. Używaj zgodnie z obowiązującymi
przepisami prawa i regulaminami oprogramowania KWHotel.