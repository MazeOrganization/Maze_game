import React, { useEffect } from 'react';
import { Route, Routes } from 'react-router-dom';
import { Maze } from "./components/Maze";
import { Layout } from './components/Layout';
import AppStore from './stores/AppStore';
import './custom.css';

const App = () => {
  const appStore = new AppStore();

  useEffect(() => {
    window.addEventListener("keydown", appStore.handleMove);
  }, [appStore]);

    return (
      <Layout>
        <Routes>
          <Route path="/" element={<Maze appStore={appStore } />} />
        </Routes>
      </Layout>
    );
  };

export default App;
