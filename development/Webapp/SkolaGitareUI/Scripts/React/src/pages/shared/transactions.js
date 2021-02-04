import React from "react";
import { Redirect } from "react-router-dom";


import { AuthConsumer } from "../../authContext";

const Transactions = () => (
    <AuthConsumer>
        {({ authenticated, user }) =>
            authenticated ? (
                <div>
                    ProfilePage

                </div>
            ) : (
                    <Redirect to="/" />

                )}
    </AuthConsumer>
);

export default Transactions;