from Pawn_class import Pawn
from Game_class import Game

class Offensive_Entity(Pawn) :

    def __init__(self,id,x, y, width, height, health,speed = 1, is_collidable = False, shape = "square", weight = 10, render = True ,damage = 100, image =  "C:/Users/user/Documents/COURS/C#/Projet/bricks-studio/assets/monster.png") : #ajouter arg pour image
        super().__init__(id, x, y, width, height, health, speed, is_collidable, shape , weight, image)
        self.damage = damage

    def deal_damage(self, target):
        target.health -= self.damage

    def handle_offensive_entities():

        for offensive_Entity in Game.entities:
            if not type(offensive_Entity).__name__ == "Offensive_Entity": continue

            collide = offensive_Entity.collision.is_colliding_any()
            if collide:

                for collision in collide:

                    if type(collision).__name__ == "str": continue

                    try:
                        offensive_Entity.deal_damage(collision)
                    except AttributeError:
                        pass