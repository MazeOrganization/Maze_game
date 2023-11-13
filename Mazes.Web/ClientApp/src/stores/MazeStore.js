import { makeAutoObservable, runInAction } from 'mobx';

export default class MazeStore {
    maze = null;
    solution = null;
    
    constructor() {
      makeAutoObservable(this)
    }
  
    async fetchMaze() {
      const response = await fetch('maze');
      const data = await response.json();
      runInAction(() => {
        this.maze = data;
      });
    }

    async solveMaze() {
      const maze = this.maze;
      if (!maze) {
        return;
      }

      const response = await fetch(`maze/${maze.id}/solve`);
      const data = await response.json();
      runInAction(() => {
        if (data) {
          for (var i = 0; i < data.length; i++) {
            const solutionCell = data[i];
            if (solutionCell.x >= 0 && solutionCell.x < maze.board.length && solutionCell.y >= 0 && solutionCell.y < maze.board.length) {
              maze.board[solutionCell.y][solutionCell.x].isSolution = true;
            }
          }
        }
      });
    }
  }
  