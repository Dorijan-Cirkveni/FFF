import React from "react";
import { Redirect } from "react-router-dom";
import { AuthConsumer } from "../../authContext";
import ProfileComponent from '../../components/shared/Profile'

const Profile = () => (
    <AuthConsumer>
        {({ authenticated, user, accessToken }) =>
            authenticated ? (
                <ProfileComponent user={user} accessToken={accessToken} />
            ) : (
                 <Redirect to="/" />

                )}
    </AuthConsumer>
);

export default Profile;