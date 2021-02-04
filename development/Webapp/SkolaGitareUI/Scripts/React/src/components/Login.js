import React from "react";

import { AuthConsumer } from "../authContext";
import Button from '@material-ui/core/Button';


const Login = (props) => {
    console.log('Gin');
    return (
        <AuthConsumer>
            {({ initiateLogin }) => (
                <Button className={props.className} onClick={() => initiateLogin(props)}>
                    Prijava
                </Button>
            )}
        </AuthConsumer>
    )
};

export default Login;