# Praktická maturitní zkouška

**Střední průmyslová škola elektrotechnická, Praha 2, Ječná 30**
**Školní rok 2022/2023**
---
Jméno a příjimeni: Petr Barabáš
Třída: C4a
---

## Úvod
Mým úkolem bylo zpracovat databázi pro cestovní ruch. Tuto databázi jsem měl vytvořit na základě reálného příkladu pro web.
Databázi jsem vytvářel pro web Airbnb(https://www.airbnb.cz/).

Pro přečtení kompletní Analýzy či Návrhu atd.. viz src/Uvod/Analyza/Analyza.txt
                                               viz.src/Uvod/Navrh/Navrh.txt

Problém jsem se rozhodl řešit v MSSQL Server s vyžutimí MSSQL Managment Studia, jako návrhové prostředí jsem 
využil Oracle DataModeler.


## E-R model
Logický model se nachází v img/Logical/logical_scheme.png
Relační schéma se nachází v img/Relation/relation_scheme.png

## Entitní integrita
př:
Každá entita obsahuje uměle vytvořený primární klíč, označený jako `id`, 
který se s každým dalším záznamem inkrementuje


## Doménová integrita
vše viz. src/Omezení/Doménové/dom_omezení.txt


## Referenční integrita
př:
viz. src/Omezení/Refernční/ref_omezení.txt

## Indexy 
- Databáze má pro každou entitu pouze indexy vytvořené pro primární klíče, 
další indexy 
int_name_apartment, int_location_name, int_user_name, int_location_city_name
viz. ukázka ve scriptu sql/mainQuery.sql

## Pohledy
- Návrh obsahuje pohledy:
reservation_count, user_photo_count, apartment_review_scores

reservation_count: viz. src/View/reservation_count_view.txt
user_photo_count: viz src/View/user_photo_count_view.txt
apartment_review_scores: src/View/apartment_review_scores_view.txt


## Triggery
- Databáze obsahuje triggery:
prevent_self_review_delete, prevent_low_score_update, create_review_backup_insert

prevent_self_review_delete viz. src/Triggers/prevent_self_review_delete_trigger.txt
prevent_low_score_update viz. src/Triggers/prevent_low_score_update_trigger.txt
create_review_backup_insert viz. src/Triggers/create_review_backup_insert_trigger.txt


## Uložené procedury
- Databáze obsahuje procedury:
 get_apartment_reservation_count, get_location_review_average, get_user_review_count
 
 get_apartment_reservation_count viz. src/Procedurs/get_apartment_reservation_count_procedure.txt
 get_location_review_average viz. src/Procedurs/get_location_review_average_procedure.txt
 get_user_review_count viz. src/Procedurs/ get_user_review_count_procedure.txt


## Přístupové údaje do databáze
př:
- Přístupové údaje jsou volně konfigurovatelné v souboru config/conf.txt


## Import struktury databáze a dat od zadavatele
př:
Nejprve je nutno si vytvořit novou databázi, čistou, bez jakýchkoliv dat...
Poté do této databáze nahrát soubor, který se nachází v /sql/structure.sql ...
Pokud si přejete načíst do databáze testovací data, je nutno nahrát ještě soubor /sql/data/dataknacteni.csv ('dataknacteni' nahraďte nazvem dat, které chcete načíst!)

## Klientská aplikace
- Databáze neobsahuje klientskou aplikaci

## Požadavky na spuštění
př:
- Oracle DataModeler, rok vydání 2014 a více ... 
- MSSQL Server, rok vydání 2014 a více ... 
- připojení k internetu alespoň 2Mb/s ...


## Návod na instalaci a ovládání aplikace
př:
Uživatel by si měl vytvořit databázi a nahrát do ní strukturu, dle kroku "Import struktury databáze 
a dat od zadavatele" ...
Poté se přihlásit předdefinovaným uživatelem, nebo si vytvořit vlastního pomocí SQL příkazů ...
Měl by upravit konfigurační soubor klientské aplikace, aby odpovídal jeho podmínkám ...
Dále nahrát obsah složky src na server a navštívit adresu serveru ... 
Přihlásit se a může začít pracovat ... 

## Závěr
př:
Tento systém by po menších úpravách mohl být převeden na jiný databázový systém, 
klientská aplikace není zabezpečená, 
počítá se s tím, že klient byl proškolen o používání této aplikace.

