import pygame
from Keys_class import Keys
import json
import os

class Game() :

    entities = []
    screen_width = 1280
    screen_height = 720
    screen = pygame.display.set_mode((screen_width, screen_height))
    clock = pygame.time.Clock()
    running = True
    dt = 0
    keys = Keys()
    directory = os.getcwd()

    def render():
        for entity in reversed(Game.entities):
            if entity.render:
                Game.screen.blit(entity.image, entity.rect)
        
    def reset_keys():
        
        for key in vars(Game.keys):
            setattr(Game.keys, key, False)

    def check_alive():

        for entity in Game.entities:
            if type(entity).__name__ != "Pawn" and type(entity).__name__ != "Offensive_Entity": continue
            if entity.health <= 0: entity.die()

    def load_entities(path):
        with open(path, 'r') as file:
            entities = json.load(file)  
        return entities
    def clean_entities():
        Game.entities[4:] = []# remove a partir du 4eme element pour garder les border


    def keydown_handle(event):
        if event.key == pygame.K_BACKSPACE:
            Game.keys.BACKSPACE = True
        if event.key == pygame.K_TAB:
            Game.keys.TAB = True
        if event.key == pygame.K_RETURN:
            Game.keys.ENTRE = True
        if event.key == pygame.K_PAUSE:
            Game.keys.PAUSE = True
        if event.key == pygame.K_ESCAPE:
            Game.keys.ECHAPE = True
        if event.key == pygame.K_SPACE:
            Game.keys.ESPACE = True
        if event.key == pygame.K_COMMA:
            Game.keys.COMMA = True
        if event.key == pygame.K_MINUS:
            Game.keys.MINUS = True
        if event.key == pygame.K_PERIOD:
            Game.keys.PERIOD = True
        if event.key == pygame.K_SLASH:
            Game.keys.SLASH = True
        if event.key == pygame.K_0:
            Game.keys.NUM_0 = True
        if event.key == pygame.K_1:
            Game.keys.NUM_1 = True
        if event.key == pygame.K_2:
            Game.keys.NUM_2 = True
        if event.key == pygame.K_3:
            Game.keys.NUM_3 = True
        if event.key == pygame.K_4:
            Game.keys.NUM_4 = True
        if event.key == pygame.K_5:
            Game.keys.NUM_5 = True
        if event.key == pygame.K_6:
            Game.keys.NUM_6 = True
        if event.key == pygame.K_7:
            Game.keys.NUM_7 = True
        if event.key == pygame.K_8:
            Game.keys.NUM_8 = True
        if event.key == pygame.K_9:
            Game.keys.NUM_9 = True
        if event.key == pygame.K_SEMICOLON:
            Game.keys.SEMICOLON = True
        if event.key == pygame.K_EQUALS:
            Game.keys.EQUALS = True
        if event.key == pygame.K_AT:
            Game.keys.AT = True
        if event.key == pygame.K_LEFTBRACKET:
            Game.keys.LEFTBRACKET = True
        if event.key == pygame.K_BACKSLASH:
            Game.keys.BACKSLASH = True
        if event.key == pygame.K_RIGHTBRACKET:
            Game.keys.RIGHTBRACKET = True
        if event.key == pygame.K_a:
            Game.keys.A = True
        if event.key == pygame.K_b:
            Game.keys.B = True
        if event.key == pygame.K_c:
            Game.keys.C = True
        if event.key == pygame.K_d:
            Game.keys.D = True
        if event.key == pygame.K_e:
            Game.keys.E = True
        if event.key == pygame.K_f:
            Game.keys.F = True
        if event.key == pygame.K_g:
            Game.keys.G = True
        if event.key == pygame.K_h:
            Game.keys.H = True
        if event.key == pygame.K_i:
            Game.keys.I = True
        if event.key == pygame.K_j:
            Game.keys.J = True
        if event.key == pygame.K_k:
            Game.keys.K = True
        if event.key == pygame.K_l:
            Game.keys.L = True
        if event.key == pygame.K_m:
            Game.keys.M = True
        if event.key == pygame.K_n:
            Game.keys.N = True
        if event.key == pygame.K_o:
            Game.keys.O = True
        if event.key == pygame.K_p:
            Game.keys.P = True
        if event.key == pygame.K_q:
            Game.keys.Q = True
        if event.key == pygame.K_r:
            Game.keys.R = True
        if event.key == pygame.K_s:
            Game.keys.S = True
        if event.key == pygame.K_t:
            Game.keys.T = True
        if event.key == pygame.K_u:
            Game.keys.U = True
        if event.key == pygame.K_v:
            Game.keys.V = True
        if event.key == pygame.K_w:
            Game.keys.W = True
        if event.key == pygame.K_x:
            Game.keys.X = True
        if event.key == pygame.K_y:
            Game.keys.Y = True
        if event.key == pygame.K_z:
            Game.keys.Z = True
        if event.key == pygame.K_DELETE:
            Game.keys.DELETE = True
        if event.key == pygame.K_KP_PERIOD:
            Game.keys.KP_PERIOD = True
        if event.key == pygame.K_KP_DIVIDE:
            Game.keys.KP_DIVIDE = True
        if event.key == pygame.K_KP_MULTIPLY:
            Game.keys.KP_MULTIPLY = True
        if event.key == pygame.K_KP_MINUS:
            Game.keys.KP_MINUS = True
        if event.key == pygame.K_KP_PLUS:
            Game.keys.KP_PLUS = True
        if event.key == pygame.K_KP_ENTER:
            Game.keys.KP_ENTER = True
        if event.key == pygame.K_UP:
            Game.keys.UP = True
        if event.key == pygame.K_DOWN:
            Game.keys.DOWN = True
        if event.key == pygame.K_RIGHT:
            Game.keys.RIGHT = True
        if event.key == pygame.K_LEFT:
            Game.keys.LEFT = True
        if event.key == pygame.K_INSERT:
            Game.keys.INSERT = True
        if event.key == pygame.K_HOME:
            Game.keys.HOME = True
        if event.key == pygame.K_END:
            Game.keys.END = True
        if event.key == pygame.K_PAGEUP:
            Game.keys.PAGEUP = True
        if event.key == pygame.K_PAGEDOWN:
            Game.keys.PAGEDOWN = True
        if event.key == pygame.K_F1:
            Game.keys.F1 = True
        if event.key == pygame.K_F2:
            Game.keys.F2 = True
        if event.key == pygame.K_F3:
            Game.keys.F3 = True
        if event.key == pygame.K_F4:
            Game.keys.F4 = True
        if event.key == pygame.K_F5:
            Game.keys.F5 = True
        if event.key == pygame.K_F6:
            Game.keys.F6 = True
        if event.key == pygame.K_F7:
            Game.keys.F7 = True
        if event.key == pygame.K_F8:
            Game.keys.F8 = True
        if event.key == pygame.K_F9:
            Game.keys.F9 = True
        if event.key == pygame.K_F10:
            Game.keys.F10 = True
        if event.key == pygame.K_F11:
            Game.keys.F11 = True
        if event.key == pygame.K_F12:
            Game.keys.F12 = True
        if event.key == pygame.K_NUMLOCK:
            Game.keys.NUMLOCK = True
        if event.key == pygame.K_CAPSLOCK:
            Game.keys.CAPSLOCK = True
        if event.key == pygame.K_RSHIFT:
            Game.keys.RSHIFT = True
        if event.key == pygame.K_LSHIFT:
            Game.keys.LSHIFT = True
        if event.key == pygame.K_RCTRL:
            Game.keys.RCTRL = True
        if event.key == pygame.K_LCTRL:
            Game.keys.LCTRL = True
        if event.key == pygame.K_RALT:
            Game.keys.RALT = True
        if event.key == pygame.K_LALT:
            Game.keys.LALT = True