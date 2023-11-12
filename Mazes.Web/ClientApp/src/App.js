import React, { useEffect } from 'react';
import { Route, Routes } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { Maze } from "./components/Maze";
import { Layout } from './components/Layout';
import AppStore from './stores/AppStore';
import './custom.css';

const App = observer(() => {
  const appStore = new AppStore();

  useEffect(() => {
    window.addEventListener("keydown", appStore.handleMove);
  }, []);

    return (
      <Layout>
        <Routes>
          <Route path="/" element={<Maze appStore={appStore } />} />
        </Routes>
      </Layout>
    );
  });

export default App;
