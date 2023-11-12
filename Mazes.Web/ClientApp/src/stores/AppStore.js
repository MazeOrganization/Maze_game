import PlayerStore from "./PlayerStore";
import MazeStore from "./MazeStore";

export default class AppStore {
    playerStore = new PlayerStore();
    mazeStore = new MazeStore();
  
    handleMove = (e) => {
      e.preventDefault();
      const key = e.code;
  
      const maze = this.mazeStore.maze;
      if (!maze) {
        return;
      }
      
      const userPosition = this.playerStore.userPosition;
      const [x, y] = userPosition;
      const cell = maze.board[y][x];
    
      if ((key === "ArrowUp" || key === "KeyW") && !cell.isUpperActive) {
        this.playerStore.setUserPosition(x, y - 1);
        return;
      }
      if ((key === "ArrowRight" || key === "KeyD") && !cell.isRightActive) {
        this.playerStore.setUserPosition(x + 1, y);
        return;
      }
      if ((key === "ArrowDown" || key === "KeyS") && !cell.isLowerActive) {
        this.playerStore.setUserPosition(x, y + 1);
        return;
      }
      if ((key === "ArrowLeft" || key === "KeyA") && !cell.isLeftActive) {
        this.playerStore.setUserPosition(x - 1, y);
        return;
      }
    };
  }