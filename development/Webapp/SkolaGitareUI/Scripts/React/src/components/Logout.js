import React from "react";

import { AuthConsumer } from "../authContext";

import Button from '@material-ui/core/Button';

const Logout = (props) => (
    <AuthConsumer>
        {({ logout }) => (
            <Button className={props.className} onClick={logout}>
                Odjava
            </Button>
        )}
    </AuthConsumer>
);

export default Logout;