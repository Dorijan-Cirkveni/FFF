import React from 'react';
import Button from '@material-ui/core/Button';
import Can from "../Can";
import { AuthConsumer } from "../../authContext";


export default function EditProfile(props) {
    const [show, setShow] = React.useState(false);

    const handleClick = () => {
        setShow(!show);
    };

    return (
        <AuthConsumer>
            {({ user }) => (
                <div>



                    <Button>Promijeni lozinku</Button>
                    <Button>Promijeni email</Button>
                    <Can
                        role={user.role}
                        perform="tariff:edit"
                        data={{
                            userId: user.id,
                        }}
                        yes={() => (

                            <Button >
                                PRomijeni tarifu
                            </Button>
                        )}
                    />




                </div>
            )}
        </AuthConsumer>
    );
}
