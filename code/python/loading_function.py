from Structure_class import Structure
import pygame
from OffensiveStructure_class import Offensive_Structure

from OffensiveEntity_class import Offensive_Entity

from Game_class import Game

from Pawn_class import Pawn

def instance_entities(levels,level):
    for entity in levels[level]["Entities"]:

        if entity['type'] == 'structure':
            instance = Structure(entity['id'], entity['x']*1.71, entity['y']*2.07, entity['width']*1.71, entity['height']*2.07,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['image'] )
            Game.entities.append(instance)
        elif entity['type'] == 'offensive_structure':
            instance = Offensive_Structure(entity['id'], entity['x']*1.71, entity['y']*2.07, entity['width']*1.71, entity['height']*2.07,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['damage'],entity['image'])
            Game.entities.append(instance)
        elif entity['type'] == 'offensive_entity':
            instance = Offensive_Entity(entity['id'], entity['x']*1.71, entity['y']*2.07, entity['width']*1.71, entity['height']*2.07,entity["max_health"],entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['damage'],entity['image'],entity['Track'])
            instance.image = pygame.transform.flip(instance.image,True,False)
            Game.entities.append(instance)
        elif entity['type'] == 'zone':
            instance = Structure(entity['id'], entity['x']*1.71, entity['y']*2.07, entity['width']*1.71, entity['height']*2.07,entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'], entity['render'],entity['image'],True)
            Game.entities.append(instance)
        elif entity['type'] == 'Player':
            Player = Pawn(entity['id'], entity['x']*1.71, entity['y']*2.07, entity['width']*1.71, entity['height']*2.07,entity['max_health'],entity['speed'],entity['is_collidable'] , entity['shape'],entity['weight'],entity['image'])
            Game.entities.append(Player)
            Player.set_weapon()
        if entity["has_weapon"]:
            try:
                instance.set_weapon()
            except NameError: pass
    return Player