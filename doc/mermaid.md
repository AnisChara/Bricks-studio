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
       + list ~action~ actions
    }

    class Declencheur {

        + nom
        + list ~event~ events  
    }

    class Action {

        +string nom
        +string function
        +string description
    }

    class Event { 
        
        +string nom
        +string function
        +string description
    }

     Projet "1" -->"n" Node




     

     