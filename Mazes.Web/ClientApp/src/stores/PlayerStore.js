import { makeAutoObservable, runInAction } from 'mobx';

export default class PlayerStore {
    userPosition = [0, 0];
    solved = false;
    time = 0;
    timeInterval = null;
  
    constructor() {
      makeAutoObservable(this);
    }
  
    setUserPosition(x, y) {
      runInAction(() => {
        this.userPosition[0] = x;
        this.userPosition[1] = y;
      });
    }

    setSolved(value) {
      runInAction(() => {
        this.solved = value;
      });
    }

    resetTime() {
      runInAction(() => {
        this.time = 0;
      });
    }

    startTime() {
      this.timeInterval = setInterval(() => {
        runInAction(() => {
          this.time += 10;
        });
      }, 10);
    }

    stopTime() {
      clearInterval(this.timeInterval);
      this.timeInterval = null;
    }
  }