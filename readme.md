# 2D side-scrolling game
## 🎮 Demo
[Play the WebGL Demo][HERE](https://staging.d3h857w88qolqv.amplifyapp.com)  
WASD Space - Movement  
E - Inventory  
LM - Shoot/Use  
RM - Aim(only for aimable item)  
## Development logs
### 26 Feb, 2026
**15:00 PM** - Initiate base game.  
**16:00 PM** - Design time instance to be restrictly one instance(Singleton) with public interface to manipulate world time  
**18:40 PM** - Add clock gui on top right corner to track world time instance  
**7:10 PM** - Add global event action and triggerer  
**9:10 PM** - Add base GUI and Inventory system  
    - Inventory interface (add, remove, clear)  
    - Scrollable inventory page   
    - Base item's data as scriptable object
### 27 Feb, 2026  
**-- 2:00 AM --**  
**10:30 AM** - Add Inventory GUI functionality  
    - Draggable (GUI)  
    - Swap (Inventory logic)  
**12:00 PM** - Overall Inventory GUI functionality  
    - Quantity label  
    - Item Icon
    - Add remove item functionality(Dropzone)  
**12:45 PM** - Test inventory interface by integrate with global events  
**1:30 PM** - Create service to offload the GUI logic  
**4:00 PM** - Separate inventory to hotbar, chest, and normal inventory  
**5.30 PM** - Design slime AI  
**6.50 PM** - Add damage system(hitbox and hurtbox handle)  
**7.20 PM** - Add bow and projectile behaviour  
**-- 8.00 PM--**  
### 28 Feb, 2026
**4:00 AM** - Add Aim indicator for aimable item  
**5:00 AM** - Add Slime dead effect(Mini Slime summon)  
**7:00 AM** - Crafting page UI mock  
**8:30 AM** - Design crafting system  
**--9:30 AM--**  
**12:40 PM** - Add crafting logic  
**2:40 PM** - Implement crafting logic to GUI  
**2:55 PM** - Add item description GUI  
**4:00 PM** - Add select functionality to inventory system  
**5:40 PM** - Add crafting table  
    - Offload base crafting logic into service  
    - Inject Crafting provider directly into GUI(Bad practice)  

### 1 Mar, 2026
**4:40 AM** - Refactor Item  
    - Separate interface(IUsable, IAimable, IPlaceable)  
    - Add consumable item  
    - Add placeable item  
**8:35 AM** - Add Health GUI
**9:30 AM** - Refactor enemy class  
    - Abstract factory  
    - Object Pooling  
**-- 4:00 PM --**  
**6.00 PM** - Add chest GUI(Third page of the book)  
**8.30 PM** - Refine  
    - Add label to event
    - Add mini slime(After refactor)  
    - Add more recipe  
    - Add wand type tool(Using blood)  
**-- 11.30 PM--** 
