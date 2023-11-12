import { makeAutoObservable, runInAction } from 'mobx';

export default class PlayerStore {
    userPosition = [0, 0];
  
    constructor() {
      makeAutoObservable(this);
    }
  
    setUserPosition(x, y) {
      runInAction(() => {
        this.userPosition[0] = x;
        this.userPosition[1] = y;
      });
    }
  }