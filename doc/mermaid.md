# Diagramme de classes

```mermaid
classDiagram
    class Projet  {
        + String nom
        + string emplacement
        + string version
        
    }

    class Node{
        + list~mechanique~
        + list~declencheur~
        + nom
        

    }

    class Mechanique{

       + nom 
       + list ~action~

    }

    class Declencheur {

        + nom
        + list ~event~   
        
         }

    class Asset {

    + string coordonné
    + double variable
}    

    class Entity {

    + string coordonné
    + double variable
}    

     class Player

     class Ennemi


     Projet "1" -->"n" Node

     Entity <|-- Player
     Entity <|-- Ennemi




     

     