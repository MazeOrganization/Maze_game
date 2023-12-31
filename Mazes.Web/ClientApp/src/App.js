import React, { useEffect, useMemo } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppStore from './stores/AppStore';
import { Congratulations } from './components/Congratulations';
import { Time } from './components/Time';
import { Layout } from './components/Layout';
import { Maze } from "./components/Maze";
import './custom.css';

const App = () => {
  const appStore = useMemo(() => new AppStore(), []);

  useEffect(() => {
    window.addEventListener("keydown", appStore.handleMove);
  }, [appStore]);

  useEffect(() => {
    appStore.mazeStore.fetchMaze();
  }, [appStore.mazeStore]);

    //let text = 'Use  ↑ ↓ → ← or WASD to help parents find their child! Buttons will help you go back to the start, try a new maze or see the solution.';
    //var curr = 0;
    //var Write = function write() {
    //        var elem = document.getElementById('target');
    //        elem.textContent = elem.textContent + text.charAt(curr);
    //        curr++;
    //        if (curr < text.length)
    //            window.setTimeout(write, 60);
    //}

    //Write();

    return (
      <Layout>
        <div style={{ width: '600px', margin: '0 auto', textAlign: 'center' }}>
          <div id="target" style={{ color: 'white', marginTop: '40px', marginBottom: '40px' }}>'Use  ↑ ↓ → ← or WASD to help parents find their child! Buttons will help you go back to the start, try a new maze or see the solution.'</div>
            <div style={{ display: 'flex', minWidth: '300px', marginTop: '30px', marginBottom: '10px', justifyContent: 'space-between' }}>
              <button style={{marginRight: '10px'}} onClick={() => {
                appStore.playerStore.setUserPosition(0, 0);
                appStore.playerStore.setSolved(false);
                appStore.mazeStore.clearSolution();
                appStore.playerStore.stopTime();
                appStore.playerStore.resetTime();
              }}>Reset</button>
              <button style={{marginRight: '10px'}} onClick={() => {
                appStore.playerStore.setUserPosition(0, 0);
                appStore.playerStore.setSolved(false);
                appStore.playerStore.stopTime();
                appStore.playerStore.resetTime();
                appStore.mazeStore.fetchMaze();
              }}>New Maze</button>
              <button onClick={() => {
                if (appStore.mazeStore.solution || appStore.playerStore.solved) {
                  return;
                }
                appStore.mazeStore.solveMaze();
                appStore.playerStore.stopTime();
              }}>Solve</button>
            </div>
          </div>
          <Congratulations playerStore={appStore.playerStore} />
        <Routes>
          <Route path="/" element={<Maze appStore={appStore } />} />
        </Routes>
        <Time playerStore={appStore.playerStore} />
      </Layout>
    );
  };

export default App;
