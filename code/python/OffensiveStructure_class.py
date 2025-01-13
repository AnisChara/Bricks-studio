from Structure_class import Structure
from Game_class import Game

class Offensive_Structure(Structure) :

    def __init__(self,id, x, y, width, height, speed = 10, is_collidable = False, shape = "square", weight = 10, render = True , damage = 100) : #ajouter arg pour image
        super().__init__(id,x, y, width, height,  speed, is_collidable, shape, weight, render, image="C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/images.png" )
        self.damage = damage

    def deal_damage(self, target):
        target.health -= self.damage

    def handle_offensive_structures():

        for offensive_Structure in Game.entities:
            if not type(offensive_Structure).__name__ == "Offensive_Structure": continue

            collide = offensive_Structure.collision.is_colliding_any()
            if collide:

                for collision in collide:

                    if type(collision).__name__ == "str": continue

                    try:
                        offensive_Structure.deal_damage(collision)
                    except AttributeError:
                        pass