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
       + list ~string~ actions
    }

    class Declencheur {

        + nom
        + list ~string~ events  
    }



     Projet "1" -->"n" Node




     

     