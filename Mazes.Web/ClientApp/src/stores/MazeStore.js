import { makeAutoObservable, runInAction } from 'mobx';

export default class MazeStore {
    maze = null;
    
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
  }
  