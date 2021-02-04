import React from "react";

import Auth from './Auth';
import Header from './Header';


const App = () => {

    return (
        <div style={{ display: 'block', position: 'fixed', overflow: "auto", width: '100%  ', }}>
           <Auth>
                <Header />
          </Auth>
        </div>
    )
};

export default App;