import uuid
from Game_class import Game
class Emitter() :

    def __init__(self, shooter) : #ajouter arg pour image
        self.shooter = shooter
        self.direction = shooter.direction
        self.x = self.shooter.rect.x + self.shooter.rect.width
        self.y = self.shooter.rect.y + self.shooter.rect.height / 2

    def update_position(self,auto = True):

        if auto :self.direction = self.shooter.direction

        if self.direction == 'right':
            self.x = self.shooter.rect.x + self.shooter.rect.width
            self.y = self.shooter.rect.y + self.shooter.rect.height / 2
        elif self.direction == 'left':
            self.x = self.shooter.rect.x
            self.y = self.shooter.rect.y + self.shooter.rect.height / 2
        elif self.direction == 'top':
            self.x = self.shooter.rect.x + self.shooter.rect.width / 2
            self.y = self.shooter.rect.y
        elif self.direction == 'bottom':
            self.x = self.shooter.rect.x + self.shooter.rect.width / 2
            self.y = self.shooter.rect.y + self.shooter.rect.height
        elif self.direction == "top-right":
            self.x = self.shooter.rect.x + self.shooter.rect.width
            self.y = self.shooter.rect.y
        elif self.direction == "top-left":
            self.x = self.shooter.rect.x
            self.y = self.shooter.rect.y
        elif self.direction == "bottom-right":
            self.x = self.shooter.rect.x + self.shooter.rect.width
            self.y = self.shooter.rect.y + self.shooter.rect.height
        elif self.direction == "bottom-left":
            self.x = self.shooter.rect.x
            self.y = self.shooter.rect.y + self.shooter.rect.height

    def shoot(self,projectile_width,projectile_height,projectile_speed,projectile_damage,projectile_direction = None,projectile_weight = 0): #parametre de direction
        from Projectile_class import Projectile

        if projectile_direction == None : projectile_direction = self.shooter.direction
        if projectile_direction != None : self.direction = projectile_direction

        self.update_position(False)

        if self.direction == 'right':
            projectile_x = self.x
            projectile_y = self.y-projectile_height/2
        elif self.direction == 'left':
            projectile_x = self.x - projectile_width
            projectile_y = self.y-projectile_height/2
        elif self.direction == 'top':
            projectile_x = self.x-projectile_width/2
            projectile_y = self.y - projectile_height
        elif self.direction == 'bottom':
            projectile_x = self.x-projectile_width/2
            projectile_y = self.y
        elif self.direction == "top-right":
            projectile_x = self.x
            projectile_y = self.y - projectile_height
        elif self.direction == "top-left":
            projectile_x = self.x - projectile_width
            projectile_y = self.y - projectile_height
        elif self.direction == "bottom-right":
            projectile_x = self.x
            projectile_y = self.y
        elif self.direction == "bottom-left":
            projectile_x = self.x - projectile_width
            projectile_y = self.y

        projectile = Projectile(uuid.uuid4(),projectile_x,projectile_y,projectile_width,projectile_height,projectile_speed,True,"square",projectile_weight,self,projectile_direction,projectile_damage)
        projectile.collision.collision_exception.append(self.shooter)
        Game.entities.append(projectile)