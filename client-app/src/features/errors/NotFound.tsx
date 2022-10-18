import React from "react";
import {Button, Header, Segment} from "semantic-ui-react";
import {Link} from "react-router-dom";

export default function NotFound() {
    return (
        <Segment placeholder>
            <Header icon>
                Oops - we've looked everywhere but could not find this.
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/activities' primary>
                    Return to activities page
                </Button>
            </Segment.Inline>
        </Segment>
    )
}