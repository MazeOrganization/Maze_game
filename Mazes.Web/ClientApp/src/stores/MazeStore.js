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

    clearSolution(){
      runInAction(() => {
        this.solution = null;
      });
    }

    async solveMaze() {
      const maze = this.maze;
      if (!maze) {
        return;
      }

      const response = await fetch(`maze/${maze.id}/solve`);
      const data = await response.json();

      const mappedSolution = Array(maze.board.length);
      for (var y = 0; y < maze.board.length; y++) {
        mappedSolution[y] = Array(maze.board.length);
        for (var x = 0; x < maze.board.length; x++) {
          mappedSolution[y][x] = false;
        }
      }

      for (var i = 0; i < data.length; i++) {
        const solutionCell = data[i];
        mappedSolution[solutionCell.y][solutionCell.x] = true;
      }

      runInAction(() => {
        this.solution = mappedSolution;
      });
    }
  }
  