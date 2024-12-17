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

    class Entity {

    +int id
    +string type
    +string collision_type
    +bool is_collidable
    +int x
    +int y
    +int velocity
    +int width
    +int height
    +string image
    +bool gravity
    +bool jump
    +bool can_shoot
}    


     Projet "1" -->"n" Node




     

     