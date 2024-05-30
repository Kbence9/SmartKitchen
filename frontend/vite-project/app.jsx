import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import './index.css';
import './pages/Home';


function App(){

    const router = createBrowserRouter([
        {
            path: '/',
            element: <Layout />,
            children: [
                {
                    path: '/',
                    element: <Home />
                }
            ]
        }
    ])
    
    return <RouterProvider router={router} />;

};


export default App;