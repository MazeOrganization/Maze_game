import React, { useEffect, useMemo } from 'react';
import { Route, Routes } from 'react-router-dom';
import AppStore from './stores/AppStore';
import { Congratulations } from './components/Congratulations';
import { Layout } from './components/Layout';
import { Maze } from "./components/Maze";
import './custom.css';

const App = () => {
  const appStore = useMemo(() => new AppStore());

  useEffect(() => {
    window.addEventListener("keydown", appStore.handleMove);
  }, [appStore]);

  useEffect(() => {
    appStore.mazeStore.fetchMaze();
  }, [appStore.mazeStore]);

    return (
      <Layout>
        <div style={{marginBottom: '20px'}}>
          <p>On desktop use the keys to move, reach the red square to solve a maze</p>
          <button style={{marginRight: '10px'}} onClick={() => {
            appStore.playerStore.setUserPosition(0, 0);
            appStore.playerStore.setSolved(false);
          }}>Reset</button>
          <button onClick={() => {
            appStore.playerStore.setUserPosition(0, 0);
            appStore.playerStore.setSolved(false);
            appStore.mazeStore.fetchMaze();
          }}>New Maze</button> 
          <Congratulations playerStore={appStore.playerStore} />
        </div>
        <Routes>
          <Route path="/" element={<Maze appStore={appStore } />} />
        </Routes>
      </Layout>
    );
  };

export default App;
