from Collision_class import Collision
from Game_class import Game

class SquareCollision(Collision):
    def __init__(self, entity, collision_exception):
        super().__init__("square", entity, collision_exception)

    def is_colliding_any(self):
        result = []
        for other_entity in Game.entities:

            if not other_entity.is_collidable: continue
            if self.entity.id == other_entity.id :continue

            collide = self.is_colliding(other_entity)
            if collide:
                result.append(other_entity)
                result.append(collide)
                result.append(other_entity.id)
                
        if len(result) > 0:
            return result
        return False
        

    def is_colliding(self, other_entity):
        """
        Vérifie si l'entité actuelle entre en collision avec une autre entité,
        avec une tolérance de 1 pixel pour les collisions.
        """
        tolerance = 1  # Tolérance de 1 pixel

        # Collision avec un autre carré
        if other_entity.shape == "square":
            if (
                self.entity.rect.x <= other_entity.rect.x + other_entity.rect.width + tolerance and
                self.entity.rect.x + self.entity.rect.width >= other_entity.rect.x - tolerance and
                self.entity.rect.y <= other_entity.rect.y + other_entity.rect.height + tolerance and
                self.entity.rect.y + self.entity.rect.height >= other_entity.rect.y - tolerance
            ):
                return self.get_collision_side(other_entity)

        # Collision avec un cercle
        elif other_entity.shape == "circle":
            square = self.entity.rect  # Carré de l'entité actuelle
            circle_center = other_entity.rect.center  # Centre du cercle
            circle_radius = other_entity.rect.width / 2  # Rayon du cercle (en supposant un cercle dans un carré)

            # Trouver le point le plus proche du cercle dans le carré
            closest_x = max(square.x, min(circle_center[0], square.x + square.width))
            closest_y = max(square.y, min(circle_center[1], square.y + square.height))

            # Calculer la distance entre le centre du cercle et ce point le plus proche
            distance_x = circle_center[0] - closest_x
            distance_y = circle_center[1] - closest_y

            # Calculer la distance au carré (pas besoin de racine carrée pour comparaison)
            distance_squared = distance_x ** 2 + distance_y ** 2

            # Vérifier si la distance est inférieure ou égale au rayon du cercle au carré
            if (distance_squared <= (circle_radius + tolerance) ** 2) :
                return self.get_collision_side(other_entity)

        return False  # Pas de collision si le type est inconnu

    def get_collision_side(self, other_entity):

        # Calcul des distances entre les centres
        dx = self.entity.rect.centerx - other_entity.rect.centerx
        dy = self.entity.rect.centery - other_entity.rect.centery

        # Calcul des chevauchements sur les axes x et y
        overlap_x = (self.entity.rect.width / 2) + (other_entity.rect.width / 2) - abs(dx)
        overlap_y = (self.entity.rect.height / 2) + (other_entity.rect.height / 2) - abs(dy)

        # Déterminer si la collision est verticale ou horizontale
        if overlap_x < overlap_y:
            # Collision horizontale (gauche ou droite)
            if dx > 0:
                return "left"  # L'entité est à droite de l'autre
            else:
                return "right"  # L'entité est à gauche de l'autre
        else:
            # Collision verticale (haut ou bas)
            if dy > 0:
                return "top"  # L'entité est en dessous de l'autre
            else:
                return "bottom"  # L'entité est au-dessus de l'autre
