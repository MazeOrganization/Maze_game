import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Maze } from "./components/Maze";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/maze',
    element: <Maze />
  }
];

export default AppRoutes;
